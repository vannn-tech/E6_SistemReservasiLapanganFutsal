using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormLapangan : Form
    {
        // Ganti Connection String sesuai dengan database kamu
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        private BindingSource bindingSource = new BindingSource(); // BindingSource untuk mengelola data dan navigasi
        private DataTable dtLapangan = new DataTable(); // DataTable untuk menyimpan data lapangan yang diambil dari database
        private BindingNavigator bindingNavigator1; // BindingNavigator untuk navigasi data, akan diinisialisasi di SetupNavigator()

        public FormLapangan()
        {
            InitializeComponent();
        }

        private void FormLapangan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBFutsalADODataSet.vw_Lapangan' table. You can move, or remove it, as needed.
            this.vw_LapanganTableAdapter.Connection.ConnectionString = connectionString;
            SetupUI();
            SetupGrid();
            SetupNavigator();

            // Inisialisasi BindingSource dan hubungkan dengan DataTable
            bindingSource.DataSource = dtLapangan;
            dgvLapangan.DataSource = bindingSource;

            LoadData();
            BindControls(); // Bind TextBox dan ComboBox ke BindingSource agar otomatis update saat navigasi
        }

        #region UI & Setup Methods
        private void SetupUI()
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

            lblTitle.ForeColor = UITheme.TextPrimary;
            lblTotal.ForeColor = UITheme.TextSecondary;
            pnlForm.BackColor = UITheme.BgPanel;
            pnlGrid.BackColor = UITheme.BgDark;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Tersedia", "Tidak Tersedia" });
            txtLapanganID.ReadOnly = true;
        }

        private void SetupGrid()
        {
            dgvLapangan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLapangan.MultiSelect = false;
            dgvLapangan.ReadOnly = true;
            dgvLapangan.AllowUserToAddRows = false;
            dgvLapangan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SetupNavigator()
        {
            // Inisialisasi BindingNavigator dan hubungkan dengan BindingSource
            bindingNavigator1 = new BindingNavigator(true)
            {
                BindingSource = bindingSource,
                Dock = DockStyle.Bottom,
                BackColor = UITheme.BgPanel
            };
            pnlGrid.Controls.Add(bindingNavigator1);
        }

        private void BindControls() // Bind TextBox dan ComboBox ke BindingSource agar otomatis update saat navigasi
        {
            txtLapanganID.DataBindings.Clear();
            txtNamaLapangan.DataBindings.Clear();
            txtLokasi.DataBindings.Clear();
            cmbStatus.DataBindings.Clear();

            if (dtLapangan.Columns.Count > 0)
            {
                txtLapanganID.DataBindings.Add("Text", bindingSource, "LapanganID", true, DataSourceUpdateMode.Never);
                txtNamaLapangan.DataBindings.Add("Text", bindingSource, "NamaLapangan");
                txtLokasi.DataBindings.Add("Text", bindingSource, "Lokasi");
                cmbStatus.DataBindings.Add("Text", bindingSource, "Status");
            }
        }
        #endregion

        #region Data Operations (Stored Procedures)
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Panggil stored procedure sp_GetLapangan untuk mengambil data lapangan
                    SqlCommand cmd = new SqlCommand("sp_GetLapangan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // Isi DataTable dengan hasil query dan hubungkan ke BindingSource
                        dtLapangan = new DataTable();
                        da.Fill(dtLapangan);
                        bindingSource.DataSource = dtLapangan;
                    }
                }
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Panggil stored procedure sp_CountLapangan untuk menghitung total lapangan
                    SqlCommand cmd = new SqlCommand("sp_CountLapangan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblTotal.Text = "Total Lapangan: " + outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error hitung total: " + ex.Message);
            }
        }
        #endregion

        #region Event Handlers
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Panggil stored procedure sp_InsertLapangan untuk menyimpan data lapangan baru
                    SqlCommand cmd = new SqlCommand("sp_InsertLapangan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NamaLapangan", txtNamaLapangan.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lokasi", txtLokasi.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

                    SqlParameter pNewID = new SqlParameter("@NewID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pNewID);
                    cmd.Parameters.Add(pPesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (pPesan.Value.ToString() == "OK")
                    {
                        MessageBox.Show("✅ Berhasil simpan!");
                        LoadData();
                    }
                    else MessageBox.Show("⚠ " + pPesan.Value.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLapanganID.Text)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateLapangan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LapanganID", int.Parse(txtLapanganID.Text));
                    cmd.Parameters.AddWithValue("@NamaLapangan", txtNamaLapangan.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lokasi", txtLokasi.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

                    SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pPesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("✅ Data diperbarui!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLapanganID.Text)) return;

            if (MessageBox.Show("Hapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_DeleteLapangan", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LapanganID", int.Parse(txtLapanganID.Text));

                        SqlParameter pPesan = new SqlParameter("@Pesan", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(pPesan);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        LoadData();
                        BersihkanForm();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SearchLapangan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Keyword", txtCari.Text.Trim());

                    SqlParameter pJml = new SqlParameter("@JumlahHasil", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pJml);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtLapangan = new DataTable();
                    da.Fill(dtLapangan);
                    bindingSource.DataSource = dtLapangan;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // --- FUNGSI RESET DATA (Modul 9 Langkah 9) ---
        private void btnReset_Click(object sender, EventArgs e) // Event handler untuk tombol Reset, menghapus data di tabel Lapangan dan mengisi ulang dari backup
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Urutan penghapusan harus dari tabel 'Anak' ke tabel 'Induk'
                string query = @"
        IF OBJECT_ID('dbo.Lapangan_Backup') IS NOT NULL
        BEGIN
            -- 1. Hapus data di tabel yang memiliki Relasi (Foreign Key) dulu
            DELETE FROM dbo.Reservasi;
            DELETE FROM dbo.Jadwal;
            
            -- 2. Baru hapus data di tabel utama
            DELETE FROM dbo.Lapangan;
            
            -- 3. Aktifkan Identity Insert agar ID dari backup bisa masuk
            SET IDENTITY_INSERT dbo.Lapangan ON;
            
            INSERT INTO dbo.Lapangan (LapanganID, NamaLapangan, Lokasi, Status)
            SELECT LapanganID, NamaLapangan, Lokasi, Status FROM dbo.Lapangan_Backup;
            
            SET IDENTITY_INSERT dbo.Lapangan OFF;
        END";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            MessageBox.Show("Data berhasil direset!");
        }

        // --- FUNGSI TEST INJECTION (Modul 9 Tahap 1 & 2) ---
        private void btnTestInjection_Click(object sender, EventArgs e) // Event handler untuk tombol Test Injection, melakukan update dengan query yang rentan terhadap SQL Injection menggunakan input dari txtNamaLapangan
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // KERENTANAN: String Concatenation
                    string query = "UPDATE Lapangan SET Lokasi='DIHACK' WHERE NamaLapangan='" + txtNamaLapangan.Text + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int result = cmd.ExecuteNonQuery();
                        MessageBox.Show(result + " baris terupdate!", "Hasil Injeksi");
                    }
                }
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            LoadData();
        }

        private void dgvLapangan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < bindingSource.Count)
                bindingSource.Position = e.RowIndex;
        }
        #endregion

        private void BersihkanForm()
        {
            txtLapanganID.Clear();
            txtNamaLapangan.Clear();
            txtLokasi.Clear();
            cmbStatus.SelectedIndex = -1;
            txtCari.Clear();
            if (bindingSource.Count > 0) bindingSource.Position = 0;
        }

        private void txtNamaLapangan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}