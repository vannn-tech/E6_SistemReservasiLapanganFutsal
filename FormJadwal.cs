using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormJadwal : Form // Form untuk manajemen reservasi, memungkinkan admin melihat, menambah, mengubah status, dan menghapus reservasi yang ada
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

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
            UITheme.StyleTextBox(txtJadwalID);
            UITheme.StyleTextBox(txtCari);
            UITheme.StyleComboBox(cmbLapangan);
            UITheme.StyleComboBox(cmbJam);
            UITheme.StyleComboBox(cmbStatusJadwal);
            UITheme.StyleDataGridView(dgvJadwal);

            lblTitle.ForeColor  = UITheme.TextPrimary;
            lblTotal.ForeColor  = UITheme.TextSecondary;
            pnlForm.BackColor   = UITheme.BgPanel;
            pnlGrid.BackColor   = UITheme.BgDark;

            txtJadwalID.ReadOnly = true;
            MuatComboLapangan();
            MuatComboJam();
            cmbStatusJadwal.Items.AddRange(new string[] { "Tersedia", "Dipesan" });
            dtpTanggal.Value = DateTime.Today;
            TampilkanData();
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
                        cmbLapangan.Items.Add(new { ID = reader["LapanganID"], Text = reader["NamaLapangan"].ToString() });
                    cmbLapangan.DisplayMember = "Text";
                    cmbLapangan.ValueMember   = "ID";
                }
                catch { }
            }
        }

        private void MuatComboJam()
        {
            string[] slots = { "08:00","09:00","10:00","11:00","12:00",
                               "13:00","14:00","15:00","16:00","17:00","18:00","19:00","20:00","21:00" };
            cmbJam.Items.AddRange(slots);
        }

        private void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT j.JadwalID, l.NamaLapangan, j.Tanggal,
                                            LEFT(CAST(j.Jam AS VARCHAR),5) AS Jam, j.Status
                                     FROM Jadwal j
                                     JOIN Lapangan l ON j.LapanganID = l.LapanganID
                                     ORDER BY j.Tanggal, j.Jam";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJadwal.DataSource         = dt;
                    dgvJadwal.ReadOnly            = true;
                    dgvJadwal.AllowUserToAddRows  = false;
                    dgvJadwal.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
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
                int total    = (int)new SqlCommand("SELECT COUNT(*) FROM Jadwal", conn).ExecuteScalar();
                int tersedia = (int)new SqlCommand("SELECT COUNT(*) FROM Jadwal WHERE Status='Tersedia'", conn).ExecuteScalar();
                lblTotal.Text = $"Total Jadwal: {total}   |   Tersedia: {tersedia}";
            }
            catch { }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbLapangan.SelectedIndex < 0 || cmbJam.SelectedIndex < 0 || cmbStatusJadwal.SelectedIndex < 0)
            {
                MessageBox.Show("Lapangan, Jam, dan Status wajib dipilih!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int lapanganID = (int)((dynamic)cmbLapangan.SelectedItem).ID;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES (@lapID, @tgl, @jam, @status)", conn);
                    cmd.Parameters.AddWithValue("@lapID",  lapanganID);
                    cmd.Parameters.AddWithValue("@tgl",    dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@jam",    cmbJam.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatusJadwal.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅  Jadwal berhasil ditambahkan!", "Sukses",
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

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJadwalID.Text))
            {
                MessageBox.Show("Pilih jadwal di tabel terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Yakin ingin mengubah jadwal ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            int lapanganID = (int)((dynamic)cmbLapangan.SelectedItem).ID;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Jadwal SET LapanganID=@lapID, Tanggal=@tgl, Jam=@jam, Status=@status WHERE JadwalID=@id", conn);
                    cmd.Parameters.AddWithValue("@id",     txtJadwalID.Text);
                    cmd.Parameters.AddWithValue("@lapID",  lapanganID);
                    cmd.Parameters.AddWithValue("@tgl",    dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@jam",    cmbJam.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatusJadwal.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅  Jadwal berhasil diperbarui!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengubah: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJadwalID.Text))
            {
                MessageBox.Show("Pilih jadwal yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Hapus jadwal ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Jadwal WHERE JadwalID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", txtJadwalID.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Jadwal berhasil dihapus!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BersihkanForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus: " + ex.Message +
                        "\n(Jadwal sudah dipakai di Reservasi)", "Error",
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
                    string query = @"SELECT j.JadwalID, l.NamaLapangan, j.Tanggal,
                                            LEFT(CAST(j.Jam AS VARCHAR),5) AS Jam, j.Status
                                     FROM Jadwal j
                                     JOIN Lapangan l ON j.LapanganID = l.LapanganID
                                     WHERE l.NamaLapangan LIKE @cari
                                        OR CAST(j.Tanggal AS VARCHAR) LIKE @cari
                                        OR j.Status LIKE @cari
                                     ORDER BY j.Tanggal, j.Jam";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cari", "%" + txtCari.Text.Trim() + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJadwal.DataSource = dt;
                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Data tidak ditemukan.", "Pencarian",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row  = dgvJadwal.Rows[e.RowIndex];
            txtJadwalID.Text     = row.Cells["JadwalID"].Value.ToString();
            cmbJam.Text          = row.Cells["Jam"].Value.ToString().Substring(0, 5);
            cmbStatusJadwal.Text = row.Cells["Status"].Value.ToString();

            if (DateTime.TryParse(row.Cells["Tanggal"].Value.ToString(), out DateTime tgl))
                dtpTanggal.Value = tgl;

            string namaLap = row.Cells["NamaLapangan"].Value.ToString();
            foreach (var item in cmbLapangan.Items)
            {
                if (((dynamic)item).Text == namaLap)
                {
                    cmbLapangan.SelectedItem = item;
                    break;
                }
            }
        }

        private void BersihkanForm()
        {
            txtJadwalID.Clear();
            cmbLapangan.SelectedIndex     = -1;
            cmbJam.SelectedIndex          = -1;
            cmbStatusJadwal.SelectedIndex = -1;
            dtpTanggal.Value              = DateTime.Today;
        }
    }
}
