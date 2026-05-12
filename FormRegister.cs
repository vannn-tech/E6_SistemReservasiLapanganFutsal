using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormRegister : Form // Form untuk registrasi akun baru, hanya untuk role 'User'
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True"; // Ganti dengan string koneksi yang sesuai dengan database Anda 

        public FormRegister() // constructor untuk inisialisasi komponen form
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e) // event handler untuk load form, mengatur tema dan gaya visual
        {
            UITheme.ApplyForm(this);
            LogoHelper.ApplyLogo(picLogo, 60);
            UITheme.StyleButtonPrimary(btnDaftar);
            UITheme.StyleButtonSecondary(btnKembali);
            UITheme.StyleTextBox(txtNama);
            UITheme.StyleTextBox(txtUsername);
            UITheme.StyleTextBox(txtPassword);
            UITheme.StyleTextBox(txtKonfirmasi);

            pnlHeader.BackColor  = UITheme.BgPanel;
            pnlForm.BackColor    = UITheme.BgDark;

            lblPesan.Visible        = false;
            txtPassword.PasswordChar    = '*';
            txtKonfirmasi.PasswordChar  = '*';
        }

        // ── Tombol DAFTAR ─────────────────────────────────────────
        private void btnDaftar_Click(object sender, EventArgs e) // event handler untuk tombol Daftar, melakukan validasi input dan menyimpan data ke database
        {
            lblPesan.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNama.Text)      ||
                string.IsNullOrWhiteSpace(txtUsername.Text)  ||
                string.IsNullOrWhiteSpace(txtPassword.Text)  ||
                string.IsNullOrWhiteSpace(txtKonfirmasi.Text))
            {
                TampilkanPesan("Semua field wajib diisi!", false);
                return;
            }

            if (txtUsername.Text.Trim().Length < 4)
            {
                TampilkanPesan("Username minimal 4 karakter!", false);
                return;
            }

            if (txtPassword.Text != txtKonfirmasi.Text)
            {
                TampilkanPesan("Password dan konfirmasi tidak cocok!", false);
                txtKonfirmasi.Clear();
                txtKonfirmasi.Focus();
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                TampilkanPesan("Password minimal 6 karakter!", false);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Cek username sudah dipakai
                    string cekQuery = "SELECT COUNT(*) FROM UserAccount WHERE Username=@user";
                    SqlCommand cekCmd = new SqlCommand(cekQuery, conn);
                    cekCmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                    int jumlah = (int)cekCmd.ExecuteScalar();

                    if (jumlah > 0)
                    {
                        TampilkanPesan("Username sudah digunakan, pilih username lain!", false);
                        txtUsername.Focus();
                        return;
                    }

                    // INSERT akun baru dengan RoleUser = 'User'
                    string insertQuery =
                        @"INSERT INTO UserAccount (Nama, Username, PasswordHash, RoleUser)
                          VALUES (@nama, @user, @pass, 'User')";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                    insertCmd.ExecuteNonQuery();

                    TampilkanPesan("✅  Akun berhasil dibuat! Silakan login.", true);
                    BersihkanForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Tombol KEMBALI ────────────────────────────────────────
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ── Helper ────────────────────────────────────────────────
        private void TampilkanPesan(string pesan, bool sukses)
        {
            lblPesan.ForeColor = sukses
                ? UITheme.AccentGreen
                : UITheme.AccentRed;
            lblPesan.Text    = pesan;
            lblPesan.Visible = true;
        }

        private void BersihkanForm()
        {
            txtNama.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtKonfirmasi.Clear();
            txtNama.Focus();
        }
    }
}
