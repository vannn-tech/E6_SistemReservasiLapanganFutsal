using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormLogin : Form
    {
        string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            LogoHelper.ApplyLogo(picLogo, 70);
            UITheme.StyleButtonPrimary(btnLogin);
            UITheme.StyleButtonSecondary(btnRegister);
            UITheme.StyleTextBox(txtUsername);
            UITheme.StyleTextBox(txtPassword);
            UITheme.StyleButtonSecondary(btnKembali);

            pnlHeader.BackColor      = UITheme.BgPanel;
            pnlForm.BackColor        = UITheme.BgDark;
            lblError.Visible         = false; //
            txtPassword.PasswordChar = '*';
            cbShowPassword.ForeColor = UITheme.TextSecondary;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowError("Username dan password wajib diisi!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try 
                {
                    // Panggil stored procedure sp_Login dengan parameter username dan password hash
                    SqlCommand cmd = new SqlCommand("sp_Login", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username",     txtUsername.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string role   = reader["RoleUser"].ToString();
                        int    userID = Convert.ToInt32(reader["UserID"]);
                        string nama   = reader["Nama"].ToString();
                        reader.Close();

                        this.Hide();
                        if (role == "Admin")
                        {
                            FormAdmin adminForm = new FormAdmin();
                            adminForm.FormClosed += (s, a) => this.Close();
                            adminForm.Show();
                        }
                        else
                        {
                            FormUser userForm = new FormUser(userID, nama);
                            userForm.FormClosed += (s, a) => this.Close();
                            userForm.Show();
                        }
                    }
                    else
                    {
                        reader.Close();
                        ShowError("Username atau password salah!");
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi database gagal:\n" + ex.Message,
                        "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister regForm = new FormRegister();
            regForm.ShowDialog();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormDashboard fd = new FormDashboard();
            fd.Show();
            this.Close();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cbShowPassword.Checked ? '\0' : '*';
        }

        private void ShowError(string msg)
        {
            lblError.Text    = "⚠  " + msg;
            lblError.Visible = true;
        }
    }
}
