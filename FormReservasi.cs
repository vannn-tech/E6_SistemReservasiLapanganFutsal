using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormReservasi : Form
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        // ── Sesuai Modul 9 Langkah 2 ─────────────────────────────
        private BindingSource bindingSource = new BindingSource();
        private DataTable     dtReservasi   = new DataTable();

        // ── BindingNavigator (Modul 8 Langkah 2) ─────────────────
        private BindingNavigator bindingNavigator1;

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

            lblTitle.ForeColor = UITheme.TextPrimary;
            lblTotal.ForeColor = UITheme.TextSecondary;
            pnlForm.BackColor  = UITheme.BgPanel;
            pnlGrid.BackColor  = UITheme.BgDark;

            txtReservasiID.ReadOnly      = true;
            txtTanggalReservasi.ReadOnly = true;
            txtTanggalReservasi.Text     = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cmbStatus.Items.AddRange(new string[] { "Aktif", "Selesai", "Dibatalkan" });

            MuatComboUser();
            MuatComboLapangan();

            // Setting Grid — sesuai Modul 9 Langkah 3
            dgvReservasi.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvReservasi.MultiSelect         = false;
            dgvReservasi.ReadOnly            = true;
            dgvReservasi.AllowUserToAddRows  = false;
            dgvReservasi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // BindingNavigator — sesuai Modul 8 Langkah 2
            bindingNavigator1 = new BindingNavigator(true)
            {
                BindingSource = bindingSource,
                Dock          = DockStyle.Bottom,
                BackColor     = UITheme.BgPanel
            };
            pnlGrid.Controls.Add(bindingNavigator1);

            // call LoadData first, then bind controls
            bindingSource.DataSource = dtReservasi;
            dgvReservasi.DataSource = bindingSource;

            LoadData();     // fills dtReservasi and calls BindControls()
        }

        // ── BindControls — sesuai Modul 9 Langkah 5 ─────────────
        private void BindControls()
        {
            txtReservasiID.DataBindings.Clear();
            txtTanggalReservasi.DataBindings.Clear();
            cmbStatus.DataBindings.Clear();

            txtReservasiID.DataBindings.Add("Text",      bindingSource, "ReservasiID");
            txtTanggalReservasi.DataBindings.Add("Text", bindingSource, "TglReservasi");
            cmbStatus.DataBindings.Add("Text",           bindingSource, "Status");
        }

        private void MuatComboUser()
        {
            cmbUser.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT UserID, Nama, Username FROM UserAccount WHERE RoleUser='User' ORDER BY Nama", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbUser.Items.Add(new {
                            ID   = (int)reader["UserID"],
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
                        "SELECT LapanganID, NamaLapangan FROM Lapangan WHERE Status='Tersedia' ORDER BY LapanganID", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cmbLapangan.Items.Add(new {
                            ID   = (int)reader["LapanganID"],
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
                    // Ambil dari VIEW vwJadwalTersedia
                    SqlCommand cmd = new SqlCommand(
                        "SELECT JadwalID, Tanggal, Jam FROM vwJadwalTersedia WHERE LapanganID=@lapID ORDER BY Tanggal, Jam",
                        conn);
                    cmd.Parameters.AddWithValue("@lapID", lapanganID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tgl = Convert.ToDateTime(reader["Tanggal"]).ToString("dd/MM/yyyy");
                        string jam = reader["Jam"].ToString().Substring(0, 5);
                        cmbJadwal.Items.Add(new { ID = (int)reader["JadwalID"], Text = tgl + " — " + jam });
                    }
                    cmbJadwal.DisplayMember = "Text";
                    cmbJadwal.ValueMember   = "ID";
                }
                catch { }
            }
        }

        // ── LoadData — sp_GetReservasi (SELECT via VIEW vwReservasi)
        //   Sesuai Modul 9 Langkah 4 & Modul 10 Langkah 1
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtReservasi = new DataTable();
                        da.Fill(dtReservasi);

                        bindingSource.DataSource = dtReservasi;
                        dgvReservasi.DataSource  = bindingSource;

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
                    SqlCommand cmd = new SqlCommand("sp_CountReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pTotal = new SqlParameter("@Total", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter pAktif = new SqlParameter("@Aktif", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pTotal);
                    cmd.Parameters.Add(pAktif);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblTotal.Text = $"Total: {pTotal.Value}   |   Aktif: {pAktif.Value}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghitung total: " + ex.Message);
            }
        }

        // ── INSERT — sp_InsertReservasi ───────────────────────────
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
                SqlCommand cmd = new SqlCommand("sp_InsertReservasi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID",     userID);
                cmd.Parameters.AddWithValue("@LapanganID", lapanganID);
                cmd.Parameters.AddWithValue("@JadwalID",   jadwalID);
                cmd.Parameters.AddWithValue("@Status",     cmbStatus.Text);

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
                    MessageBox.Show("✅  Reservasi berhasil ditambahkan!", "Sukses",
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

        // ── UPDATE STATUS — sp_UpdateStatusReservasi ──────────────
        private void btnUbahStatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReservasiID.Text))
            {
                MessageBox.Show("Pilih reservasi di tabel atau gunakan navigator!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin mengubah status reservasi ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateStatusReservasi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservasiID", int.Parse(txtReservasiID.Text));
                cmd.Parameters.AddWithValue("@Status",      cmbStatus.Text);

                SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200)
                    { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pPesan);

                conn.Open();
                cmd.ExecuteNonQuery();

                if (pPesan.Value.ToString() == "OK")
                {
                    MessageBox.Show("✅  Status berhasil diperbarui!", "Sukses",
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

        // ── DELETE — sp_DeleteReservasi ───────────────────────────
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReservasiID.Text))
            {
                MessageBox.Show("Pilih reservasi yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Hapus reservasi ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteReservasi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservasiID", int.Parse(txtReservasiID.Text));

                SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200)
                    { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pPesan);

                conn.Open();
                cmd.ExecuteNonQuery();

                if (pPesan.Value.ToString() == "OK")
                {
                    MessageBox.Show("Reservasi berhasil dihapus!", "Info",
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

        // ── SEARCH — sp_SearchReservasi ───────────────────────────
        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SearchReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Keyword", txtCari.Text.Trim());

                    SqlParameter pJml = new SqlParameter("@JumlahHasil", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pJml);

                    dtReservasi = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    da.Fill(dtReservasi);

                    bindingSource.DataSource = dtReservasi;
                    dgvReservasi.DataSource  = bindingSource;
                    BindControls();

                    int jumlah = (int)pJml.Value;
                    lblTotal.Text = $"Hasil pencarian: {jumlah} data";

                    if (jumlah == 0)
                        MessageBox.Show("Data tidak ditemukan.", "Pencarian",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // ── Klik baris DGV → sinkronkan BindingSource ────────────
        private void dgvReservasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= bindingSource.Count) return;
            bindingSource.Position = e.RowIndex;

            // Ambil JadwalID dari baris terpilih untuk keperluan Update Status
            DataRowView row = bindingSource.Current as DataRowView;
            if (row != null)
                txtJadwalIDRef.Text = row["JadwalID"]?.ToString() ?? "";
        }

        private void dgvReservasi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= bindingSource.Count) return;
            bindingSource.Position = e.RowIndex;
        }

        private void BersihkanForm()
        {
            bindingSource.Position    = -1;
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
