using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormUser : Form // Form utama untuk role User, menampilkan jadwal lapangan yang tersedia dan riwayat reservasi milik user tersebut
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        private int    _userID;
        private string _nama;

        public FormUser(int userID, string nama)
        {
            InitializeComponent();
            _userID = userID;
            _nama   = nama;
        }

        private void FormUser_Load(object sender, EventArgs e) 
        {
            UITheme.ApplyForm(this);
            LogoHelper.ApplyLogo(picLogoHeader, 40);
            UITheme.StyleButtonPrimary(btnPesan);
            UITheme.StyleButtonDanger(btnBatalkan);
            UITheme.StyleButtonSecondary(btnRefresh);
            UITheme.StyleButtonDanger(btnLogout);
            UITheme.StyleComboBox(cmbFilterLapangan);
            UITheme.StyleDataGridView(dgvJadwalTersedia);
            UITheme.StyleDataGridView(dgvRiwayat);

            pnlHeader.BackColor  = UITheme.BgPanel;
            tabControl.BackColor = UITheme.BgDark;

            // Warna tab
            tabJadwal.BackColor  = UITheme.BgDark;
            tabRiwayat.BackColor = UITheme.BgDark;

            lblSambutan.Text     = "Selamat datang, " + _nama + "!";
            lblSambutan.ForeColor = UITheme.TextPrimary;
            lblInfoJadwal.ForeColor = UITheme.TextSecondary;
            lblFilterLapangan.ForeColor = UITheme.TextSecondary;
            lblRiwayat.ForeColor = UITheme.TextMuted;

            MuatComboLapangan();
            TampilkanJadwalTersedia();
            TampilkanRiwayatSaya();
        }

        // ── Load lapangan ke ComboBox filter ─────────────────────
        private void MuatComboLapangan() // method untuk memuat daftar lapangan yang tersedia ke ComboBox filter, dengan opsi default untuk menampilkan semua lapangan
        {
            cmbFilterLapangan.Items.Clear();
            cmbFilterLapangan.Items.Add("-- Semua Lapangan --");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT LapanganID, NamaLapangan FROM Lapangan WHERE Status='Tersedia'", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbFilterLapangan.Items.Add(new {
                            ID   = reader["LapanganID"],
                            Text = reader["NamaLapangan"].ToString()
                        });
                    }
                    cmbFilterLapangan.DisplayMember = "Text";
                    cmbFilterLapangan.SelectedIndex  = 0;
                }
                catch { }
            }
        }

        // ── Tampilkan jadwal yang masih tersedia ──────────────────
        private void TampilkanJadwalTersedia() // method untuk menampilkan jadwal lapangan yang masih tersedia (status 'Tersedia' dan tanggal >= hari ini) di DataGridView, dengan informasi lapangan, tanggal, jam, dan status
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT j.JadwalID, l.NamaLapangan, l.Lokasi,
                                            j.Tanggal, LEFT(CAST(j.Jam AS VARCHAR),5) AS Jam, j.Status
                                     FROM Jadwal j
                                     JOIN Lapangan l ON j.LapanganID = l.LapanganID
                                     WHERE j.Status = 'Tersedia'
                                       AND j.Tanggal >= CAST(GETDATE() AS DATE)
                                     ORDER BY j.Tanggal, j.Jam";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJadwalTersedia.DataSource        = dt;
                    dgvJadwalTersedia.SelectionMode     = DataGridViewSelectionMode.FullRowSelect;
                    dgvJadwalTersedia.ReadOnly           = true;
                    dgvJadwalTersedia.AllowUserToAddRows = false;
                    lblInfoJadwal.Text = "Jadwal tersedia: " + dt.Rows.Count + " slot";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat jadwal: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Tampilkan riwayat reservasi milik user ini ────────────
        private void TampilkanRiwayatSaya() // method untuk menampilkan riwayat reservasi milik user ini di DataGridView, dengan informasi lapangan, tanggal & jam jadwal, tanggal reservasi, dan status (Aktif/Dibatalkan/Selesai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT r.ReservasiID, l.NamaLapangan,
                                            CAST(j.Tanggal AS VARCHAR(10)) + ' ' + LEFT(CAST(j.Jam AS VARCHAR),5) AS JadwalInfo,
                                            CONVERT(VARCHAR(16), r.TanggalReservasi, 120) AS TglReservasi,
                                            r.Status
                                     FROM Reservasi r
                                     JOIN Lapangan l ON r.LapanganID = l.LapanganID
                                     JOIN Jadwal j   ON r.JadwalID = j.JadwalID
                                     WHERE r.UserID = @uid
                                     ORDER BY r.TanggalReservasi DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@uid", _userID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRiwayat.DataSource        = dt;
                    dgvRiwayat.SelectionMode     = DataGridViewSelectionMode.FullRowSelect;
                    dgvRiwayat.ReadOnly           = true;
                    dgvRiwayat.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat riwayat: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Tombol PESAN ──────────────────────────────────────────
        private void btnPesan_Click(object sender, EventArgs e) // event handler untuk tombol Pesan, melakukan validasi pemilihan jadwal, konfirmasi pemesanan, dan menyimpan data reservasi ke database serta mengupdate status jadwal menjadi 'Dipesan'
        {
            if (dgvJadwalTersedia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih jadwal yang ingin dipesan terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvJadwalTersedia.SelectedRows[0];
            int    jadwalID     = Convert.ToInt32(row.Cells["JadwalID"].Value);
            string namaLapangan = row.Cells["NamaLapangan"].Value.ToString();
            string tanggal      = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("dd/MM/yyyy");
            string jam          = row.Cells["Jam"].Value.ToString();

            // Ambil LapanganID dari DB
            int lapanganID = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT LapanganID FROM Jadwal WHERE JadwalID=@jid", conn);
                    cmd.Parameters.AddWithValue("@jid", jadwalID);
                    lapanganID = (int)cmd.ExecuteScalar();
                }
                catch { }
            }

            DialogResult dr = MessageBox.Show(
                $"Konfirmasi Pemesanan\n\nLapangan : {namaLapangan}\nTanggal  : {tanggal}\nJam      : {jam}\n\nLanjutkan?",
                "Konfirmasi Pesan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO Reservasi (UserID, LapanganID, JadwalID, Status)
                                           VALUES (@uid, @lapID, @jadID, 'Aktif')";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@uid",   _userID);
                    cmd.Parameters.AddWithValue("@lapID", lapanganID);
                    cmd.Parameters.AddWithValue("@jadID", jadwalID);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdJ = new SqlCommand(
                        "UPDATE Jadwal SET Status='Dipesan' WHERE JadwalID=@jid", conn);
                    cmdJ.Parameters.AddWithValue("@jid", jadwalID);
                    cmdJ.ExecuteNonQuery();

                    MessageBox.Show("✅  Reservasi berhasil! Jadwal telah dipesan.", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilkanJadwalTersedia();
                    TampilkanRiwayatSaya();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membuat reservasi: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Tombol BATALKAN ───────────────────────────────────────
        private void btnBatalkan_Click(object sender, EventArgs e) // event handler untuk tombol Batalkan, melakukan validasi pemilihan reservasi, konfirmasi pembatalan, dan mengupdate status reservasi menjadi 'Dibatalkan' serta mengupdate status jadwal menjadi 'Tersedia' jika pembatalan berhasil
        {
            if (dgvRiwayat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih reservasi yang ingin dibatalkan!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row  = dgvRiwayat.SelectedRows[0];
            string status        = row.Cells["Status"].Value.ToString();
            int    reservasiID   = Convert.ToInt32(row.Cells["ReservasiID"].Value);

            if (status != "Aktif")
            {
                MessageBox.Show("Hanya reservasi berstatus 'Aktif' yang dapat dibatalkan.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("Yakin ingin membatalkan reservasi ini?",
                "Konfirmasi Batalkan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdJad = new SqlCommand(
                        "SELECT JadwalID FROM Reservasi WHERE ReservasiID=@rid", conn);
                    cmdJad.Parameters.AddWithValue("@rid", reservasiID);
                    int jadwalID = (int)cmdJad.ExecuteScalar();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Reservasi SET Status='Dibatalkan' WHERE ReservasiID=@rid", conn);
                    cmd.Parameters.AddWithValue("@rid", reservasiID);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdJ = new SqlCommand(
                        "UPDATE Jadwal SET Status='Tersedia' WHERE JadwalID=@jid", conn);
                    cmdJ.Parameters.AddWithValue("@jid", jadwalID);
                    cmdJ.ExecuteNonQuery();

                    MessageBox.Show("Reservasi berhasil dibatalkan.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilkanJadwalTersedia();
                    TampilkanRiwayatSaya();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membatalkan: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Filter lapangan ───────────────────────────────────────
        private void cmbFilterLapangan_SelectedIndexChanged(object sender, EventArgs e) // event handler untuk perubahan pilihan di ComboBox filter lapangan, menampilkan jadwal yang tersedia sesuai dengan lapangan yang dipilih atau semua lapangan jika opsi default dipilih
        {
            if (cmbFilterLapangan.SelectedIndex <= 0)
            {
                TampilkanJadwalTersedia();
                return;
            }

            try
            {
                int lapanganID = (int)((dynamic)cmbFilterLapangan.SelectedItem).ID;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT j.JadwalID, l.NamaLapangan, l.Lokasi,
                                            j.Tanggal, LEFT(CAST(j.Jam AS VARCHAR),5) AS Jam, j.Status
                                     FROM Jadwal j
                                     JOIN Lapangan l ON j.LapanganID = l.LapanganID
                                     WHERE j.Status = 'Tersedia'
                                       AND j.LapanganID = @lapID
                                       AND j.Tanggal >= CAST(GETDATE() AS DATE)
                                     ORDER BY j.Tanggal, j.Jam";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@lapID", lapanganID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJadwalTersedia.DataSource = dt;
                    lblInfoJadwal.Text = "Jadwal tersedia: " + dt.Rows.Count + " slot";
                }
            }
            catch { }
        }

        // ── Tombol LOGOUT ─────────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Yakin ingin logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                FormDashboard fd = new FormDashboard();
                fd.Show();
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TampilkanJadwalTersedia();
            TampilkanRiwayatSaya();
        }
    }
}
