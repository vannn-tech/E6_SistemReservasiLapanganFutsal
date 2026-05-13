using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormJadwal : Form
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        // ── Sesuai Modul 9 Langkah 2 ─────────────────────────────
        private BindingSource bindingSource = new BindingSource();
        private DataTable     dtJadwal      = new DataTable();

        // ── BindingNavigator (Modul 8 Langkah 2) ─────────────────
        private BindingNavigator bindingNavigator1;

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
    }
}
