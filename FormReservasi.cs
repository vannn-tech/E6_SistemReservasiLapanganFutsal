using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormReservasi : Form
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        public FormReservasi()
        {
            InitializeComponent();
        }

        private void FormReservasi_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            UITheme.StyleButtonPrimary(btnSimpan);
            UITheme.StyleButtonSecondary(btnUbahStatus);
            UITheme.StyleButtonDanger(btnHapus);
            UITheme.StyleButtonSecondary(btnView);
            UITheme.StyleButtonSecondary(btnCari);
            UITheme.StyleTextBox(txtReservasiID);
            UITheme.StyleTextBox(txtTanggalReservasi);
            UITheme.StyleTextBox(txtCari);
            UITheme.StyleComboBox(cmbUser);
            UITheme.StyleComboBox(cmbLapangan);
            UITheme.StyleComboBox(cmbJadwal);
            UITheme.StyleComboBox(cmbStatus);
            UITheme.StyleDataGridView(dgvReservasi);

            lblTitle.ForeColor  = UITheme.TextPrimary;
            lblTotal.ForeColor  = UITheme.TextSecondary;
            pnlForm.BackColor   = UITheme.BgPanel;
            pnlGrid.BackColor   = UITheme.BgDark;

            txtReservasiID.ReadOnly      = true;
            txtTanggalReservasi.ReadOnly = true;
            txtTanggalReservasi.Text     = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cmbStatus.Items.AddRange(new string[] { "Aktif", "Selesai", "Dibatalkan" });

            MuatComboUser();
            MuatComboLapangan();
            TampilkanData();
        }

        private void MuatComboUser()
        {
            cmbUser.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT UserID, Nama, Username FROM UserAccount WHERE RoleUser='User'", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbUser.Items.Add(new {
                            ID   = reader["UserID"],
                            Text = reader["Nama"] + " (" + reader["Username"] + ")"
                        });
                    cmbUser.DisplayMember = "Text";
                    cmbUser.ValueMember   = "ID";
                }
                catch { }
            }
        }

        private void MuatComboLapangan()
        {
            cmbLapangan.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT LapanganID, NamaLapangan FROM Lapangan WHERE Status='Tersedia'", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbLapangan.Items.Add(new {
                            ID   = reader["LapanganID"],
                            Text = reader["NamaLapangan"].ToString()
                        });
                    cmbLapangan.DisplayMember = "Text";
                    cmbLapangan.ValueMember   = "ID";
                }
                catch { }
            }
        }

        private void cmbLapangan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLapangan.SelectedIndex < 0) return;
            int lapanganID = (int)((dynamic)cmbLapangan.SelectedItem).ID;
            MuatComboJadwal(lapanganID);
        }

        private void MuatComboJadwal(int lapanganID)
        {
            cmbJadwal.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT JadwalID, Tanggal, Jam FROM Jadwal WHERE LapanganID=@lapID AND Status='Tersedia' ORDER BY Tanggal, Jam",
                        conn);
                    cmd.Parameters.AddWithValue("@lapID", lapanganID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tgl = Convert.ToDateTime(reader["Tanggal"]).ToString("dd/MM/yyyy");
                        string jam = reader["Jam"].ToString().Substring(0, 5);
                        cmbJadwal.Items.Add(new { ID = reader["JadwalID"], Text = tgl + " — " + jam });
                    }
                    cmbJadwal.DisplayMember = "Text";
                    cmbJadwal.ValueMember   = "ID";
                }
                catch { }
            }
        }

        private void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT r.ReservasiID, u.Nama AS Pengguna, l.NamaLapangan,
                                            CAST(j.Tanggal AS VARCHAR(10)) + ' ' + LEFT(CAST(j.Jam AS VARCHAR),5) AS JadwalInfo,
                                            CONVERT(VARCHAR(16), r.TanggalReservasi, 120) AS TglReservasi,
                                            r.Status
                                     FROM Reservasi r
                                     JOIN UserAccount u ON r.UserID = u.UserID
                                     JOIN Lapangan l    ON r.LapanganID = l.LapanganID
                                     JOIN Jadwal j      ON r.JadwalID = j.JadwalID
                                     ORDER BY r.TanggalReservasi DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReservasi.DataSource        = dt;
                    dgvReservasi.ReadOnly           = true;
                    dgvReservasi.AllowUserToAddRows = false;
                    dgvReservasi.SelectionMode      = DataGridViewSelectionMode.FullRowSelect;
                    HitungTotal(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HitungTotal(SqlConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                int total = (int)new SqlCommand("SELECT COUNT(*) FROM Reservasi", conn).ExecuteScalar();
                int aktif = (int)new SqlCommand("SELECT COUNT(*) FROM Reservasi WHERE Status='Aktif'", conn).ExecuteScalar();
                lblTotal.Text = $"Total: {total}   |   Aktif: {aktif}";
            }
            catch { }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbUser.SelectedIndex < 0 || cmbLapangan.SelectedIndex < 0 ||
                cmbJadwal.SelectedIndex < 0 || cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userID     = (int)((dynamic)cmbUser.SelectedItem).ID;
            int lapanganID = (int)((dynamic)cmbLapangan.SelectedItem).ID;
            int jadwalID   = (int)((dynamic)cmbJadwal.SelectedItem).ID;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Reservasi (UserID, LapanganID, JadwalID, Status) VALUES (@uid, @lapID, @jadID, @status)",
                        conn);
                    cmd.Parameters.AddWithValue("@uid",    userID);
                    cmd.Parameters.AddWithValue("@lapID",  lapanganID);
                    cmd.Parameters.AddWithValue("@jadID",  jadwalID);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdJadwal = new SqlCommand(
                        "UPDATE Jadwal SET Status='Dipesan' WHERE JadwalID=@jid", conn);
                    cmdJadwal.Parameters.AddWithValue("@jid", jadwalID);
                    cmdJadwal.ExecuteNonQuery();

                    MessageBox.Show("✅  Reservasi berhasil ditambahkan!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyimpan: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUbahStatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReservasiID.Text))
            {
                MessageBox.Show("Pilih reservasi di tabel terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Yakin ingin mengubah status reservasi ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Reservasi SET Status=@status WHERE ReservasiID=@id", conn);
                    cmd.Parameters.AddWithValue("@id",     txtReservasiID.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.ExecuteNonQuery();

                    if ((cmbStatus.Text == "Dibatalkan" || cmbStatus.Text == "Selesai") &&
                        !string.IsNullOrEmpty(txtJadwalIDRef.Text))
                    {
                        SqlCommand cmdJ = new SqlCommand(
                            "UPDATE Jadwal SET Status='Tersedia' WHERE JadwalID=@jid", conn);
                        cmdJ.Parameters.AddWithValue("@jid", txtJadwalIDRef.Text);
                        cmdJ.ExecuteNonQuery();
                    }

                    MessageBox.Show("✅  Status berhasil diperbarui!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengubah status: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReservasiID.Text))
            {
                MessageBox.Show("Pilih reservasi yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Hapus reservasi ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (!string.IsNullOrEmpty(txtJadwalIDRef.Text))
                    {
                        SqlCommand cmdJ = new SqlCommand(
                            "UPDATE Jadwal SET Status='Tersedia' WHERE JadwalID=@jid", conn);
                        cmdJ.Parameters.AddWithValue("@jid", txtJadwalIDRef.Text);
                        cmdJ.ExecuteNonQuery();
                    }

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Reservasi WHERE ReservasiID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", txtReservasiID.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Reservasi berhasil dihapus!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e) => TampilkanData();

        private void btnCari_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT r.ReservasiID, u.Nama AS Pengguna, l.NamaLapangan,
                                            CAST(j.Tanggal AS VARCHAR(10)) + ' ' + LEFT(CAST(j.Jam AS VARCHAR),5) AS JadwalInfo,
                                            CONVERT(VARCHAR(16), r.TanggalReservasi, 120) AS TglReservasi, r.Status
                                     FROM Reservasi r
                                     JOIN UserAccount u ON r.UserID = u.UserID
                                     JOIN Lapangan l    ON r.LapanganID = l.LapanganID
                                     JOIN Jadwal j      ON r.JadwalID = j.JadwalID
                                     WHERE u.Nama LIKE @cari OR l.NamaLapangan LIKE @cari OR r.Status LIKE @cari
                                     ORDER BY r.TanggalReservasi DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cari", "%" + txtCari.Text.Trim() + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReservasi.DataSource = dt;
                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Data tidak ditemukan.", "Pencarian",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void dgvReservasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row      = dgvReservasi.Rows[e.RowIndex];
            txtReservasiID.Text      = row.Cells["ReservasiID"].Value.ToString();
            txtTanggalReservasi.Text = row.Cells["TglReservasi"].Value.ToString();
            cmbStatus.Text           = row.Cells["Status"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT JadwalID FROM Reservasi WHERE ReservasiID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", txtReservasiID.Text);
                    conn.Open();
                    txtJadwalIDRef.Text = cmd.ExecuteScalar().ToString();
                }
                catch { }
            }
        }

        private void BersihkanForm()
        {
            txtReservasiID.Clear();
            txtJadwalIDRef.Text       = "";
            txtTanggalReservasi.Text  = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cmbUser.SelectedIndex     = -1;
            cmbLapangan.SelectedIndex = -1;
            cmbJadwal.Items.Clear();
            cmbStatus.SelectedIndex   = -1;
        }
    }
}
