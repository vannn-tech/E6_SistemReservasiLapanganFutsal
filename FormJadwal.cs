using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;

namespace ReservasiFutsal02
{
    public partial class FormJadwal : Form
    {
        string connectionString = @"Data Source=10.200.161.237\MSSQLSERVER01;Initial Catalog=DBFutsalADO;User ID=sa;Password=jovan1532006";

        // ── Sesuai Modul 9 Langkah 2 ─────────────────────────────
        private BindingSource bindingSource = new BindingSource();
        private DataTable     dtJadwal      = new DataTable();

        // ── BindingNavigator (Modul 8 Langkah 2) ─────────────────
        private BindingNavigator bindingNavigator1;

        // ── Import Excel (Modul 14) ──────────────────────────────
        // dtImportPreview menampung data hasil baca file Excel SEBELUM
        // disimpan ke database, supaya admin bisa lihat dulu apa yang
        // akan diimport (preview) sebelum klik "Import ke Database".
        private DataTable dtImportPreview = null;
        private bool      modeImportPreview = false;

        public FormJadwal()
        {
            InitializeComponent();
        }

        private void FormJadwal_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            UITheme.StyleButtonPrimary(btnSimpan);
            UITheme.StyleButtonSecondary(btnUbah);
            UITheme.StyleButtonDanger(btnHapus);
            UITheme.StyleButtonSecondary(btnView);
            UITheme.StyleButtonSecondary(btnCari);
            UITheme.StyleButtonOrange(btnImportExcel);
            UITheme.StyleButtonPrimary(btnImportDb);
            btnImportDb.Enabled = false;
            UITheme.StyleTextBox(txtJadwalID);
            UITheme.StyleTextBox(txtCari);
            UITheme.StyleComboBox(cmbLapangan);
            UITheme.StyleComboBox(cmbJam);
            UITheme.StyleComboBox(cmbStatusJadwal);
            UITheme.StyleDataGridView(dgvJadwal);

            lblTitle.ForeColor = UITheme.TextPrimary;
            lblTotal.ForeColor = UITheme.TextSecondary;
            pnlForm.BackColor  = UITheme.BgPanel;
            pnlGrid.BackColor  = UITheme.BgDark;

            txtJadwalID.ReadOnly = true;
            MuatComboLapangan();
            MuatComboJam();
            cmbStatusJadwal.Items.AddRange(new string[] { "Tersedia", "Dipesan" });
            dtpTanggal.Value = DateTime.Today;

            // Setting Grid — sesuai Modul 9 Langkah 3
            dgvJadwal.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvJadwal.MultiSelect         = false;
            dgvJadwal.ReadOnly            = true;
            dgvJadwal.AllowUserToAddRows  = false;
            dgvJadwal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // BindingNavigator — sesuai Modul 8 Langkah 2
            bindingNavigator1 = new BindingNavigator(true)
            {
                BindingSource = bindingSource,
                Dock          = DockStyle.Bottom,
                BackColor     = UITheme.BgPanel
            };
            pnlGrid.Controls.Add(bindingNavigator1);

            // in FormJadwal_Load: remove BindControls() here
            // ...
            bindingSource.DataSource = dtJadwal;
            dgvJadwal.DataSource = bindingSource;

            // Remove this BindControls();  <-- cause of exception
            LoadData(); // LoadData will call BindControls after filling dtJadwal
        }

        // ── BindControls — sesuai Modul 9 Langkah 5 ─────────────
        private void BindControls()
        {
            txtJadwalID.DataBindings.Clear();
            cmbStatusJadwal.DataBindings.Clear();

            if (bindingSource.DataSource is DataTable dt && dt.Columns.Contains("JadwalID"))
                txtJadwalID.DataBindings.Add("Text", bindingSource, "JadwalID");
            if (bindingSource.DataSource is DataTable dt2 && dt2.Columns.Contains("Status"))
                cmbStatusJadwal.DataBindings.Add("Text", bindingSource, "Status");
        }

        private void MuatComboLapangan()
        {
            cmbLapangan.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT LapanganID, NamaLapangan FROM Lapangan ORDER BY LapanganID", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbLapangan.Items.Add(new { ID = (int)reader["LapanganID"], Text = reader["NamaLapangan"].ToString() });
                    cmbLapangan.DisplayMember = "Text";
                    cmbLapangan.ValueMember   = "ID";
                }
                catch { }
            }
        }

        private void MuatComboJam()
        {
            string[] slots = {
                "08:00","09:00","10:00","11:00","12:00",
                "13:00","14:00","15:00","16:00","17:00",
                "18:00","19:00","20:00","21:00"
            };
            cmbJam.Items.AddRange(slots);
        }

        // ── LoadData — sp_GetJadwal (SELECT via VIEW vwJadwal)
        //   Sesuai Modul 9 Langkah 4 & Modul 10 Langkah 1
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtJadwal = new DataTable();
                        da.Fill(dtJadwal);

                        bindingSource.DataSource = dtJadwal;
                        dgvJadwal.DataSource     = bindingSource;

                        BindControls();
                    }
                }
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        // ── HitungTotal — SP COUNT dengan OUTPUT Parameter ───────
        //   Sesuai Modul 10 Langkah 6
        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CountJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pTotal    = new SqlParameter("@Total",    SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter pTersedia = new SqlParameter("@Tersedia", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pTotal);
                    cmd.Parameters.Add(pTersedia);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblTotal.Text = $"Total Jadwal: {pTotal.Value}   |   Tersedia: {pTersedia.Value}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghitung total: " + ex.Message);
            }
        }

        // ── INSERT — sp_InsertJadwal ──────────────────────────────
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbLapangan.SelectedIndex < 0 || cmbJam.SelectedIndex < 0 ||
                cmbStatusJadwal.SelectedIndex < 0)
            {
                MessageBox.Show("Lapangan, Jam, dan Status wajib dipilih!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int lapanganID = (int)((dynamic)cmbLapangan.SelectedItem).ID;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LapanganID", lapanganID);
                    cmd.Parameters.AddWithValue("@Tanggal",    dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@Jam",        TimeSpan.Parse(cmbJam.Text));
                    cmd.Parameters.AddWithValue("@Status",     cmbStatusJadwal.Text);

                    SqlParameter pNewID = new SqlParameter("@NewID", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pNewID);
                    cmd.Parameters.Add(pPesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (pPesan.Value.ToString() == "OK")
                    {
                        MessageBox.Show("✅  Jadwal berhasil ditambahkan!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BersihkanForm();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("⚠  " + pPesan.Value.ToString(), "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnSimpan", ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnSimpan", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        // ── UPDATE — sp_UpdateJadwal ──────────────────────────────
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJadwalID.Text))
            {
                MessageBox.Show("Pilih jadwal di tabel atau gunakan navigator!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin mengubah jadwal ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int lapanganID = cmbLapangan.SelectedIndex >= 0
                ? (int)((dynamic)cmbLapangan.SelectedItem).ID : 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JadwalID",   int.Parse(txtJadwalID.Text));
                    cmd.Parameters.AddWithValue("@LapanganID", lapanganID);
                    cmd.Parameters.AddWithValue("@Tanggal",    dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@Jam",        TimeSpan.Parse(cmbJam.Text));
                    cmd.Parameters.AddWithValue("@Status",     cmbStatusJadwal.Text);

                    SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pPesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (pPesan.Value.ToString() == "OK")
                    {
                        MessageBox.Show("✅  Jadwal berhasil diperbarui!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BersihkanForm();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("⚠  " + pPesan.Value.ToString(), "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnUbah", ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnUbah", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        // ── DELETE — sp_DeleteJadwal ──────────────────────────────
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJadwalID.Text))
            {
                MessageBox.Show("Pilih jadwal yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Hapus jadwal ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JadwalID", int.Parse(txtJadwalID.Text));

                    SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pPesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (pPesan.Value.ToString() == "OK")
                    {
                        MessageBox.Show("Jadwal berhasil dihapus!", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BersihkanForm();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("⚠  " + pPesan.Value.ToString(), "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnHapus", ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormJadwal.btnHapus", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            LoadData();
        }

        // ── SEARCH — sp_SearchJadwal ──────────────────────────────
        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SearchJadwal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Keyword", txtCari.Text.Trim());

                    SqlParameter pJml = new SqlParameter("@JumlahHasil", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pJml);

                    dtJadwal = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    da.Fill(dtJadwal);

                    bindingSource.DataSource = dtJadwal;
                    dgvJadwal.DataSource     = bindingSource;
                    BindControls();

                    int jumlah = (int)pJml.Value;
                    lblTotal.Text = $"Hasil pencarian: {jumlah} data";

                    if (jumlah == 0)
                        MessageBox.Show("Data tidak ditemukan.", "Pencarian",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= bindingSource.Count) return;
            bindingSource.Position = e.RowIndex;

            // Sinkronkan combo Lapangan dari nama yang terpilih
            DataRowView row    = bindingSource.Current as DataRowView;
            if (row == null) return;
            string namaLap     = row["NamaLapangan"]?.ToString() ?? "";
            foreach (var item in cmbLapangan.Items)
                if (((dynamic)item).Text == namaLap) { cmbLapangan.SelectedItem = item; break; }

            if (DateTime.TryParse(row["Tanggal"]?.ToString(), out DateTime tgl))
                dtpTanggal.Value = tgl;

            cmbJam.Text = row["Jam"]?.ToString() ?? "";
        }

        private void BersihkanForm()
        {
            bindingSource.Position    = -1;
            txtJadwalID.Clear();
            cmbLapangan.SelectedIndex     = -1;
            cmbJam.SelectedIndex          = -1;
            cmbStatusJadwal.SelectedIndex = -1;
            dtpTanggal.Value              = DateTime.Today;
        }

        // ════════════════════════════════════════════════════════════
        //  IMPORT EXCEL → DATABASE   (Modul 14, disesuaikan untuk Jadwal)
        //
        //  Kenapa fitur Import Excel dipasang di Jadwal (bukan di
        //  Lapangan/Reservasi)? Karena Jadwal adalah data dengan VOLUME
        //  PALING BESAR & PALING SERING DIBUAT BERULANG (banyak slot jam
        //  x banyak tanggal x banyak lapangan) — sehingga input satu-satu
        //  lewat form paling tidak efisien di sini, dan paling diuntungkan
        //  oleh import massal dari Excel.
        //
        //  Format kolom Excel yang diharapkan (baris pertama = header):
        //      NamaLapangan | Tanggal       | Jam   | Status
        //      Lapangan A   | 25/06/2026    | 08:00 | Tersedia
        // ════════════════════════════════════════════════════════════

        // ── Langkah 1: Baca file Excel ke DataTable preview ─────────
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel Workbook|*.xlsx;*.xls" })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        });

                        DataTable dt = result.Tables[0];

                        // Validasi kolom minimal yang wajib ada
                        string[] kolomWajib = { "NamaLapangan", "Tanggal", "Jam" };
                        foreach (string k in kolomWajib)
                        {
                            if (!dt.Columns.Contains(k))
                            {
                                MessageBox.Show(
                                    "Kolom '" + k + "' tidak ditemukan di file Excel.\n" +
                                    "Pastikan header kolom: NamaLapangan, Tanggal, Jam, Status",
                                    "Format Tidak Sesuai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        dtImportPreview = dt;

                        // Tampilkan preview di grid yang sama (mode sementara — belum disimpan ke DB)
                        bindingNavigator1.Enabled = false;
                        dgvJadwal.DataSource       = dtImportPreview;
                        dgvJadwal.Enabled          = false;   // preview saja, tidak bisa diklik-edit

                        modeImportPreview = true;
                        ToggleModeImport(true);

                        lblTotal.Text = "Preview Import Excel: " + dt.Rows.Count + " baris — klik 'Import ke Database' untuk menyimpan.";
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.SimpanLog("FormJadwal.btnImportExcel", ex.Message);
                    MessageBox.Show("Gagal membaca file Excel: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Langkah 2: Simpan data preview ke database ──────────────
        //
        //  T-SQL & Transaksi (Modul 11 + 12): looping disimpan di
        //  BACKEND C# (bukan di satu Stored Procedure tunggal), karena
        //  jumlah barisnya BARU DIKETAHUI SAAT RUNTIME (tergantung isi
        //  file Excel yang diupload pengguna) — sebuah SP punya
        //  signature statis sehingga tidak praktis menerima "N baris
        //  dinamis" sekaligus. Validasi & aturan data (PK, FK, trigger
        //  trg_InsertJadwal) tetap didelegasikan ke SP/trigger di sisi
        //  database, sementara C# hanya mengatur ALUR loop + ATOMICITY
        //  lewat SqlTransaction: kalau salah satu baris gagal di tengah
        //  (misalnya melanggar constraint), SEMUA baris yang sudah
        //  ter-insert pada batch ini ikut di-ROLLBACK — supaya tidak ada
        //  hasil "separuh jadi" yang membingungkan admin.
        private void btnImportDb_Click(object sender, EventArgs e)
        {
            if (dtImportPreview == null || dtImportPreview.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data hasil import untuk disimpan.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(
                    $"Simpan {dtImportPreview.Rows.Count} baris jadwal ke database?",
                    "Konfirmasi Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int sukses = 0;
            var dilewati = new System.Collections.Generic.List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Ambil lookup NamaLapangan -> LapanganID sekali saja (di luar transaksi, hanya SELECT)
                var lapanganMap = new System.Collections.Generic.Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                using (SqlCommand cmdLap = new SqlCommand("SELECT LapanganID, NamaLapangan FROM Lapangan", conn))
                using (SqlDataReader rd = cmdLap.ExecuteReader())
                {
                    while (rd.Read())
                        lapanganMap[rd["NamaLapangan"].ToString().Trim()] = (int)rd["LapanganID"];
                }

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (DataRow row in dtImportPreview.Rows)
                    {
                        string namaLapangan = row["NamaLapangan"]?.ToString().Trim();
                        string tanggalRaw   = row["Tanggal"]?.ToString().Trim();
                        string jamRaw        = row["Jam"]?.ToString().Trim();
                        string status        = dtImportPreview.Columns.Contains("Status")
                                                    ? row["Status"]?.ToString().Trim()
                                                    : "";
                        if (string.IsNullOrEmpty(status)) status = "Tersedia";

                        // ── Validasi per baris (skip baris yang tidak valid, lanjut ke baris berikutnya) ──
                        if (string.IsNullOrEmpty(namaLapangan) || !lapanganMap.ContainsKey(namaLapangan))
                        {
                            dilewati.Add($"Lapangan '{namaLapangan}' tidak ditemukan");
                            continue;
                        }
                        if (!DateTime.TryParse(tanggalRaw, out DateTime tanggal))
                        {
                            dilewati.Add($"Tanggal '{tanggalRaw}' tidak valid (baris {namaLapangan})");
                            continue;
                        }
                        if (!TimeSpan.TryParse(jamRaw, out TimeSpan jam))
                        {
                            dilewati.Add($"Jam '{jamRaw}' tidak valid (baris {namaLapangan})");
                            continue;
                        }
                        if (status != "Tersedia" && status != "Dipesan")
                        {
                            status = "Tersedia";
                        }

                        int lapanganID = lapanganMap[namaLapangan];

                        SqlCommand cmd = new SqlCommand("sp_InsertJadwal", conn, trans);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LapanganID", lapanganID);
                        cmd.Parameters.AddWithValue("@Tanggal",    tanggal.Date);
                        cmd.Parameters.AddWithValue("@Jam",        jam);
                        cmd.Parameters.AddWithValue("@Status",     status);

                        SqlParameter pNewID = new SqlParameter("@NewID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(pNewID);
                        cmd.Parameters.Add(pPesan);

                        cmd.ExecuteNonQuery();

                        if (pPesan.Value.ToString() == "OK")
                            sukses++;
                        else
                            dilewati.Add($"{namaLapangan} {tanggal:dd/MM/yyyy} {jam} -> {pPesan.Value}");
                    }

                    trans.Commit();

                    AppLogger.SimpanAktivitas($"Import Excel Jadwal: {sukses} baris berhasil, {dilewati.Count} dilewati.");

                    string ringkasan = $"✅  Import selesai.\n\nBerhasil : {sukses} baris\nDilewati : {dilewati.Count} baris";
                    if (dilewati.Count > 0)
                        ringkasan += "\n\nContoh yang dilewati:\n- " + string.Join("\n- ",
                            dilewati.GetRange(0, Math.Min(5, dilewati.Count)));

                    MessageBox.Show(ringkasan, "Import ke Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    trans.Rollback();
                    AppLogger.SimpanLog("FormJadwal.btnImportDb", "ROLLBACK import excel : " + ex.Message);
                    MessageBox.Show("Import dibatalkan (transaksi di-rollback). SQL Error: " + ex.Message,
                        "Gagal Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    AppLogger.SimpanLog("FormJadwal.btnImportDb", "ROLLBACK import excel : " + ex.Message);
                    MessageBox.Show("Import dibatalkan (transaksi di-rollback): " + ex.Message,
                        "Gagal Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Kembali ke mode normal & refresh grid dari database
            dtImportPreview   = null;
            modeImportPreview = false;
            ToggleModeImport(false);
            bindingNavigator1.Enabled = true;
            dgvJadwal.Enabled          = true;
            LoadData();
        }

        // ── Toggle tombol CRUD saat sedang preview import ───────────
        private void ToggleModeImport(bool sedangPreview)
        {
            btnSimpan.Enabled = !sedangPreview;
            btnUbah.Enabled   = !sedangPreview;
            btnHapus.Enabled  = !sedangPreview;
            btnView.Enabled   = !sedangPreview;
            btnCari.Enabled   = !sedangPreview;
            btnImportDb.Enabled = sedangPreview;
        }
    }
}
