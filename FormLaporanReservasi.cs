using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  FormLaporanReservasi — fitur Reporting (Modul 13), disesuaikan
    //  untuk aplikasi reservasi futsal: laporan daftar reservasi
    //  dengan filter Lapangan / Status / Tahun, lalu bisa di:
    //    • Tampilkan (preview tabel)
    //    • Export CSV
    //    • Export PDF      (pakai SimplePdfWriter — tanpa dependency)
    //    • Print Preview   (pakai System.Drawing.Printing bawaan .NET)
    //
    //  CATATAN CRYSTAL REPORTS:
    //  Modul 13 aslinya memakai SAP Crystal Reports + CrystalReportViewer,
    //  yang file .rpt-nya HARUS didesain manual lewat GUI Crystal Report
    //  Designer di Visual Studio (drag & drop field, bukan sesuatu yang
    //  bisa digenerate lewat kode/teks). Supaya project ini tetap bisa
    //  langsung di-build tanpa perlu install SAP Crystal Reports runtime
    //  dulu, versi Crystal Report DIPISAH sebagai file opsional:
    //  lihat FormCetakLaporan.cs (tidak diikutkan ke .csproj secara
    //  default) beserta instruksi lengkap di README_FITUR_TAMBAHAN.md.
    // ─────────────────────────────────────────────────────────────
    public partial class FormLaporanReservasi : Form
    {
        string connectionString = @"Data Source=10.200.161.237\MSSQLSERVER01;Initial Catalog=DBFutsalADO;User ID=sa;Password=jovan1532006";

        private DataTable dtLaporan = new DataTable();

        public FormLaporanReservasi()
        {
            InitializeComponent();
        }

        private void FormLaporanReservasi_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            UITheme.StyleButtonPrimary(btnTampilkan);
            UITheme.StyleButtonSecondary(btnExportCsv);
            UITheme.StyleButtonSecondary(btnExportPdf);
            UITheme.StyleButtonSecondary(btnPrintPreview);
            UITheme.StyleComboBox(cmbLapangan);
            UITheme.StyleComboBox(cmbStatusFilter);
            UITheme.StyleComboBox(cmbTahunFilter);
            UITheme.StyleDataGridView(dgvLaporan);

            lblTitle.ForeColor = UITheme.TextPrimary;
            lblTotal.ForeColor = UITheme.TextSecondary;
            pnlFilter.BackColor = UITheme.BgPanel;
            pnlBottom.BackColor = UITheme.BgPanel;

            dgvLaporan.ReadOnly = true;
            dgvLaporan.AllowUserToAddRows = false;
            dgvLaporan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            MuatComboLapangan();

            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(new string[] { "Semua Status", "Aktif", "Selesai", "Dibatalkan" });
            cmbStatusFilter.SelectedIndex = 0;

            cmbTahunFilter.Items.Clear();
            cmbTahunFilter.Items.Add("Semua Tahun");
            int tahunIni = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
                cmbTahunFilter.Items.Add((tahunIni - i).ToString());
            cmbTahunFilter.SelectedIndex = 0;

            Tampilkan();
        }

        private void MuatComboLapangan()
        {
            cmbLapangan.Items.Clear();
            cmbLapangan.Items.Add(new { ID = 0, Text = "Semua Lapangan" });
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT LapanganID, NamaLapangan FROM Lapangan ORDER BY LapanganID", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbLapangan.Items.Add(new { ID = (int)reader["LapanganID"], Text = reader["NamaLapangan"].ToString() });
                }
                catch (Exception ex)
                {
                    AppLogger.SimpanLog("FormLaporanReservasi.MuatComboLapangan", ex.Message);
                }
            }
            cmbLapangan.DisplayMember = "Text";
            cmbLapangan.ValueMember = "ID";
            cmbLapangan.SelectedIndex = 0;
        }

        // ── Ambil data laporan dari sp_LaporanReservasi sesuai filter ────
        private void Tampilkan()
        {
            try
            {
                int lapanganID = cmbLapangan.SelectedIndex >= 0 ? (int)((dynamic)cmbLapangan.SelectedItem).ID : 0;
                string status = cmbStatusFilter.SelectedIndex <= 0 ? "" : cmbStatusFilter.Text;
                int tahun = cmbTahunFilter.SelectedIndex <= 0 ? 0 : int.Parse(cmbTahunFilter.Text);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_LaporanReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LapanganID", lapanganID);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);
                    cmd.Parameters.AddWithValue("@Tahun", tahun);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtLaporan = new DataTable();
                    da.Fill(dtLaporan);

                    dgvLaporan.DataSource = dtLaporan;
                }

                lblTotal.Text = $"Total: {dtLaporan.Rows.Count} reservasi";
            }
            catch (SqlException ex)
            {
                AppLogger.SimpanLog("FormLaporanReservasi.Tampilkan", ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormLaporanReservasi.Tampilkan", ex.Message);
                MessageBox.Show("Gagal memuat laporan: " + ex.Message);
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e) => Tampilkan();

        // ── Export CSV ───────────────────────────────────────────────
        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dtLaporan.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk di-export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV File|*.csv",
                FileName = "LaporanReservasi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // Header
                        var headers = new List<string>();
                        foreach (DataColumn col in dtLaporan.Columns) headers.Add(col.ColumnName);
                        sw.WriteLine(string.Join(",", headers));

                        // Baris data
                        foreach (DataRow row in dtLaporan.Rows)
                        {
                            var values = new List<string>();
                            foreach (var item in row.ItemArray)
                                values.Add("\"" + item.ToString().Replace("\"", "\"\"") + "\"");
                            sw.WriteLine(string.Join(",", values));
                        }
                    }
                    MessageBox.Show("✅  Export CSV berhasil:\n" + sfd.FileName, "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    AppLogger.SimpanLog("FormLaporanReservasi.ExportCsv", ex.Message);
                    MessageBox.Show("Gagal export CSV: " + ex.Message);
                }
            }
        }

        // ── Export PDF (tanpa dependency, lihat SimplePdfWriter.cs) ──
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dtLaporan.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk di-export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF File|*.pdf",
                FileName = "LaporanReservasi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var headers = new string[dtLaporan.Columns.Count];
                    for (int i = 0; i < dtLaporan.Columns.Count; i++) headers[i] = dtLaporan.Columns[i].ColumnName;

                    var rows = new List<string[]>();
                    foreach (DataRow row in dtLaporan.Rows)
                    {
                        var cells = new string[dtLaporan.Columns.Count];
                        for (int i = 0; i < dtLaporan.Columns.Count; i++) cells[i] = row[i]?.ToString() ?? "";
                        rows.Add(cells);
                    }

                    SimplePdfWriter.WriteTable(sfd.FileName, "Laporan Reservasi — ReservasiFutsal02", headers, rows);

                    MessageBox.Show("✅  Export PDF berhasil:\n" + sfd.FileName, "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    AppLogger.SimpanLog("FormLaporanReservasi.ExportPdf", ex.Message);
                    MessageBox.Show("Gagal export PDF: " + ex.Message);
                }
            }
        }

        // ── Print Preview (System.Drawing.Printing bawaan .NET) ──────
        private PrintDocument _printDoc;
        private int _printRowIndex;

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (dtLaporan.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printRowIndex = 0;
            _printDoc = new PrintDocument();
            _printDoc.PrintPage += PrintDoc_PrintPage;

            using (PrintPreviewDialog ppd = new PrintPreviewDialog { Document = _printDoc, Width = 800, Height = 600 })
            {
                ppd.ShowDialog();
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fTitle = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fHeader = new Font("Segoe UI", 9, FontStyle.Bold);
            Font fBody = new Font("Segoe UI", 8.5f);

            float y = e.MarginBounds.Top;
            g.DrawString("Laporan Reservasi — ReservasiFutsal02", fTitle, Brushes.Black, e.MarginBounds.Left, y);
            y += 30;

            float[] colWidth = { 0.06f, 0.20f, 0.18f, 0.14f, 0.10f, 0.18f, 0.14f };
            string[] headers = { "ID", "Pengguna", "Lapangan", "Tanggal", "Jam", "Tgl Reservasi", "Status" };

            float x = e.MarginBounds.Left;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], fHeader, Brushes.Black, x, y);
                x += colWidth[i] * e.MarginBounds.Width;
            }
            y += 18;
            g.DrawLine(Pens.Black, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
            y += 4;

            while (_printRowIndex < dtLaporan.Rows.Count)
            {
                if (y > e.MarginBounds.Bottom - 20)
                {
                    e.HasMorePages = true;
                    return;
                }

                DataRow row = dtLaporan.Rows[_printRowIndex];
                x = e.MarginBounds.Left;
                string[] cells =
                {
                    row["ReservasiID"].ToString(),
                    row["Pengguna"].ToString(),
                    row["NamaLapangan"].ToString(),
                    Convert.ToDateTime(row["Tanggal"]).ToString("dd/MM/yyyy"),
                    row["Jam"].ToString(),
                    Convert.ToDateTime(row["TanggalReservasi"]).ToString("dd/MM/yyyy HH:mm"),
                    row["Status"].ToString()
                };

                for (int i = 0; i < cells.Length; i++)
                {
                    g.DrawString(cells[i], fBody, Brushes.Black, x, y);
                    x += colWidth[i] * e.MarginBounds.Width;
                }

                y += 16;
                _printRowIndex++;
            }

            e.HasMorePages = false;
        }

        // ── Tombol opsional: arahkan ke Crystal Report (lihat README) ──
        private void btnCrystalReport_Click(object sender, EventArgs e)
        {

        }
    }
}
