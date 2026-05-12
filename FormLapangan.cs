using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormLapangan : Form
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        public FormLapangan()
        {
            InitializeComponent();
        }

        private void FormLapangan_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            UITheme.StyleButtonPrimary(btnSimpan);
            UITheme.StyleButtonSecondary(btnUbah);
            UITheme.StyleButtonDanger(btnHapus);
            UITheme.StyleButtonSecondary(btnTampilkanData);
            UITheme.StyleButtonSecondary(btnCari);
            UITheme.StyleTextBox(txtLapanganID);
            UITheme.StyleTextBox(txtNamaLapangan);
            UITheme.StyleTextBox(txtLokasi);
            UITheme.StyleTextBox(txtCari);
            UITheme.StyleComboBox(cmbStatus);
            UITheme.StyleDataGridView(dgvLapangan);

            lblTitle.ForeColor  = UITheme.TextPrimary;
            lblTotal.ForeColor  = UITheme.TextSecondary;
            pnlForm.BackColor   = UITheme.BgPanel;
            pnlGrid.BackColor   = UITheme.BgDark;

            cmbStatus.Items.AddRange(new string[] { "Tersedia", "Tidak Tersedia" });
            txtLapanganID.ReadOnly = true;

            // Konfigurasi DGV — cegah baris kosong & edit langsung
            dgvLapangan.AllowUserToAddRows    = false;
            dgvLapangan.AllowUserToDeleteRows = false;
            dgvLapangan.ReadOnly              = true;
            dgvLapangan.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;

            TampilkanDataDenganDataReader();
        }

        // ── Setup kolom DGV (dipanggil sekali, reuse setelahnya) ──
        private void SetupKolomDGV()
        {
            dgvLapangan.Columns.Clear();

            var colID     = new DataGridViewTextBoxColumn { Name = "LapanganID",   HeaderText = "ID",            Width = 45, ReadOnly = true };
            var colNama   = new DataGridViewTextBoxColumn { Name = "NamaLapangan", HeaderText = "Nama Lapangan", ReadOnly = true };
            var colLokasi = new DataGridViewTextBoxColumn { Name = "Lokasi",        HeaderText = "Lokasi",        ReadOnly = true };
            var colStatus = new DataGridViewTextBoxColumn { Name = "Status",        HeaderText = "Status",        Width = 110, ReadOnly = true };

            colNama.AutoSizeMode   = DataGridViewAutoSizeColumnMode.Fill;
            colLokasi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLapangan.Columns.AddRange(colID, colNama, colLokasi, colStatus);
            dgvLapangan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN E — Tampilkan Data menggunakan SqlDataReader
        // ══════════════════════════════════════════════════════════
        private void TampilkanDataDenganDataReader()
        {
            dgvLapangan.Rows.Clear();
            if (dgvLapangan.Columns.Count == 0) SetupKolomDGV();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand    cmd    = new SqlCommand(
                        "SELECT LapanganID, NamaLapangan, Lokasi, Status FROM Lapangan ORDER BY LapanganID",
                        conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dgvLapangan.Rows.Add(
                            reader["LapanganID"].ToString(),
                            reader["NamaLapangan"].ToString(),
                            reader["Lokasi"].ToString(),
                            reader["Status"].ToString()
                        );
                    }
                    reader.Close();

                    // ExecuteScalar — hitung total & tampil ke lblTotal
                    HitungTotalExecuteScalar(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── ExecuteScalar → lblTotal ──────────────────────────────
        private void HitungTotalExecuteScalar(SqlConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                int total    = (int)new SqlCommand("SELECT COUNT(*) FROM Lapangan", conn).ExecuteScalar();
                int tersedia = (int)new SqlCommand("SELECT COUNT(*) FROM Lapangan WHERE Status='Tersedia'", conn).ExecuteScalar();
                lblTotal.Text = $"Total Lapangan: {total}   |   Tersedia: {tersedia}";
            }
            catch { }
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN D — INSERT
        // ══════════════════════════════════════════════════════════
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaLapangan.Text) ||
                string.IsNullOrWhiteSpace(txtLokasi.Text) ||
                cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Nama Lapangan, Lokasi, dan Status wajib diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Lapangan (NamaLapangan, Lokasi, Status) VALUES (@nama, @lok, @stat)", conn);
                    cmd.Parameters.AddWithValue("@nama", txtNamaLapangan.Text.Trim());
                    cmd.Parameters.AddWithValue("@lok",  txtLokasi.Text.Trim());
                    cmd.Parameters.AddWithValue("@stat", cmbStatus.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅  Lapangan berhasil ditambahkan!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanDataDenganDataReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyimpan: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN D — UPDATE
        // ══════════════════════════════════════════════════════════
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLapanganID.Text))
            {
                MessageBox.Show("Pilih lapangan di tabel terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNamaLapangan.Text) ||
                string.IsNullOrWhiteSpace(txtLokasi.Text) ||
                cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Nama Lapangan, Lokasi, dan Status wajib diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Konfirmasi sebelum UPDATE
            if (MessageBox.Show("Yakin ingin mengubah data lapangan ini?",
                "Konfirmasi Ubah", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Lapangan SET NamaLapangan=@nama, Lokasi=@lok, Status=@stat WHERE LapanganID=@id", conn);
                    cmd.Parameters.AddWithValue("@id",   txtLapanganID.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNamaLapangan.Text.Trim());
                    cmd.Parameters.AddWithValue("@lok",  txtLokasi.Text.Trim());
                    cmd.Parameters.AddWithValue("@stat", cmbStatus.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅  Data lapangan berhasil diperbarui!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanDataDenganDataReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengubah: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN D — DELETE
        // ══════════════════════════════════════════════════════════
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLapanganID.Text))
            {
                MessageBox.Show("Pilih lapangan yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Konfirmasi sebelum DELETE
            if (MessageBox.Show(
                "Hapus lapangan ini?\nData jadwal & reservasi terkait juga akan terpengaruh.",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Lapangan WHERE LapanganID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", txtLapanganID.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lapangan berhasil dihapus!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanDataDenganDataReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus: " + ex.Message +
                        "\n(Lapangan masih dipakai di Jadwal/Reservasi)", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN E — Tombol Tampilkan Data
        // ══════════════════════════════════════════════════════════
        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            TampilkanDataDenganDataReader();
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN E — Pencarian
        // ══════════════════════════════════════════════════════════
        private void btnCari_Click(object sender, EventArgs e)
        {
            string keyword = txtCari.Text.Trim();

            dgvLapangan.Rows.Clear();
            if (dgvLapangan.Columns.Count == 0) SetupKolomDGV();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT LapanganID, NamaLapangan, Lokasi, Status FROM Lapangan
                          WHERE NamaLapangan LIKE @cari OR Lokasi LIKE @cari OR Status LIKE @cari
                          ORDER BY LapanganID", conn);
                    cmd.Parameters.AddWithValue("@cari", "%" + keyword + "%");
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    int jumlah = 0;
                    while (reader.Read())
                    {
                        dgvLapangan.Rows.Add(
                            reader["LapanganID"].ToString(),
                            reader["NamaLapangan"].ToString(),
                            reader["Lokasi"].ToString(),
                            reader["Status"].ToString()
                        );
                        jumlah++;
                    }
                    reader.Close();

                    lblTotal.Text = jumlah == 0
                        ? "Tidak ditemukan untuk: \"" + keyword + "\""
                        : $"Hasil pencarian: {jumlah} data ditemukan";

                    if (jumlah == 0)
                        MessageBox.Show("Data tidak ditemukan.", "Pencarian",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mencari: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════
        //  BAGIAN E — Klik baris DGV → isi TextBox
        // ══════════════════════════════════════════════════════════
        private void dgvLapangan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLapangan.Rows.Count) return;
            DataGridViewRow row  = dgvLapangan.Rows[e.RowIndex];
            txtLapanganID.Text   = row.Cells["LapanganID"].Value?.ToString()   ?? "";
            txtNamaLapangan.Text = row.Cells["NamaLapangan"].Value?.ToString() ?? "";
            txtLokasi.Text       = row.Cells["Lokasi"].Value?.ToString()       ?? "";
            cmbStatus.Text       = row.Cells["Status"].Value?.ToString()       ?? "";
        }

        private void BersihkanForm()
        {
            txtLapanganID.Clear();
            txtNamaLapangan.Clear();
            txtLokasi.Clear();
            cmbStatus.SelectedIndex = -1;
        }
    }
}
