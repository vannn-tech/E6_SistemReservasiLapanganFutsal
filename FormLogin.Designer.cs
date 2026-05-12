namespace ReservasiFutsal02
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader     = new System.Windows.Forms.Panel();
            this.picLogo       = new System.Windows.Forms.PictureBox();
            this.lblAppName    = new System.Windows.Forms.Label();
            this.lblSubtitle   = new System.Windows.Forms.Label();
            this.pnlForm       = new System.Windows.Forms.Panel();
            this.lblUsername   = new System.Windows.Forms.Label();
            this.txtUsername   = new System.Windows.Forms.TextBox();
            this.lblPassword   = new System.Windows.Forms.Label();
            this.txtPassword   = new System.Windows.Forms.TextBox();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.lblError      = new System.Windows.Forms.Label();
            this.btnLogin      = new System.Windows.Forms.Button();
            this.btnRegister   = new System.Windows.Forms.Button();
            this.btnKembali    = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.lblAppName);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock   = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 150;

            // picLogo
            this.picLogo.Location = new System.Drawing.Point(175, 18);
            this.picLogo.Name     = "picLogo";
            this.picLogo.Size     = new System.Drawing.Size(70, 70);
            this.picLogo.TabStop  = false;

            // lblAppName
            this.lblAppName.AutoSize  = false;
            this.lblAppName.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAppName.Location  = new System.Drawing.Point(10, 95);
            this.lblAppName.Size      = new System.Drawing.Size(400, 30);
            this.lblAppName.Text      = "ReservasiFutsal02";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSubtitle
            this.lblSubtitle.AutoSize  = false;
            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitle.Location  = new System.Drawing.Point(10, 125);
            this.lblSubtitle.Size      = new System.Drawing.Size(400, 20);
            this.lblSubtitle.Text      = "Masuk untuk mengakses sistem";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlForm ──────────────────────────────────────────
            this.pnlForm.Controls.Add(this.lblUsername);
            this.pnlForm.Controls.Add(this.txtUsername);
            this.pnlForm.Controls.Add(this.lblPassword);
            this.pnlForm.Controls.Add(this.txtPassword);
            this.pnlForm.Controls.Add(this.cbShowPassword);
            this.pnlForm.Controls.Add(this.lblError);
            this.pnlForm.Controls.Add(this.btnLogin);
            this.pnlForm.Controls.Add(this.btnRegister);
            this.pnlForm.Controls.Add(this.btnKembali);
            this.pnlForm.Dock    = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 28);
            this.lblUsername.Text     = "Username";
            this.lblUsername.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(40, 48);
            this.txtUsername.Name     = "txtUsername";
            this.txtUsername.Size     = new System.Drawing.Size(340, 26);
            this.txtUsername.Font     = new System.Drawing.Font("Segoe UI", 9.5F);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(40, 90);
            this.lblPassword.Text     = "Password";
            this.lblPassword.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtPassword
            this.txtPassword.Location     = new System.Drawing.Point(40, 110);
            this.txtPassword.Name         = "txtPassword";
            this.txtPassword.Size         = new System.Drawing.Size(340, 26);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Font         = new System.Drawing.Font("Segoe UI", 9.5F);

            // cbShowPassword
            this.cbShowPassword.AutoSize = true;
            this.cbShowPassword.Location = new System.Drawing.Point(40, 143);
            this.cbShowPassword.Name     = "cbShowPassword";
            this.cbShowPassword.Text     = "Tampilkan password";
            this.cbShowPassword.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);

            // lblError
            this.lblError.AutoSize  = false;
            this.lblError.Location  = new System.Drawing.Point(40, 168);
            this.lblError.Size      = new System.Drawing.Size(340, 22);
            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.Visible   = false;
            this.lblError.Text      = "";

            // btnLogin
            this.btnLogin.Location  = new System.Drawing.Point(40, 198);
            this.btnLogin.Name      = "btnLogin";
            this.btnLogin.Size      = new System.Drawing.Size(340, 42);
            this.btnLogin.Text      = "🔐   MASUK";
            this.btnLogin.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Click    += new System.EventHandler(this.btnLogin_Click);

            // btnRegister
            this.btnRegister.Location  = new System.Drawing.Point(40, 250);
            this.btnRegister.Name      = "btnRegister";
            this.btnRegister.Size      = new System.Drawing.Size(340, 35);
            this.btnRegister.Text      = "📝  Belum punya akun? Daftar di sini";
            this.btnRegister.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegister.Click    += new System.EventHandler(this.btnRegister_Click);

            // btnKembali
            this.btnKembali.Location  = new System.Drawing.Point(40, 295);
            this.btnKembali.Name      = "btnKembali";
            this.btnKembali.Size      = new System.Drawing.Size(340, 28);
            this.btnKembali.Text      = "← Kembali ke Beranda";
            this.btnKembali.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnKembali.Click    += new System.EventHandler(this.btnKembali_Click);

            // ── FormLogin ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(420, 490);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox    = false;
            this.Name           = "FormLogin";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "Login — ReservasiFutsal02";
            this.Load          += new System.EventHandler(this.FormLogin_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel       pnlHeader;
        private System.Windows.Forms.PictureBox  picLogo;
        private System.Windows.Forms.Label       lblAppName;
        private System.Windows.Forms.Label       lblSubtitle;
        private System.Windows.Forms.Panel       pnlForm;
        private System.Windows.Forms.Label       lblUsername;
        private System.Windows.Forms.TextBox     txtUsername;
        private System.Windows.Forms.Label       lblPassword;
        private System.Windows.Forms.TextBox     txtPassword;
        private System.Windows.Forms.CheckBox    cbShowPassword;
        private System.Windows.Forms.Label       lblError;
        private System.Windows.Forms.Button      btnLogin;
        private System.Windows.Forms.Button      btnRegister;
        private System.Windows.Forms.Button      btnKembali;
    }
}
