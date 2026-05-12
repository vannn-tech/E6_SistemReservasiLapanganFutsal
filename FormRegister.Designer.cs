namespace ReservasiFutsal02
{
    partial class FormRegister
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
            this.lblTitle      = new System.Windows.Forms.Label();
            this.lblSub        = new System.Windows.Forms.Label();
            this.pnlForm       = new System.Windows.Forms.Panel();
            this.lblNama       = new System.Windows.Forms.Label();
            this.txtNama       = new System.Windows.Forms.TextBox();
            this.lblUsername   = new System.Windows.Forms.Label();
            this.txtUsername   = new System.Windows.Forms.TextBox();
            this.lblPassword   = new System.Windows.Forms.Label();
            this.txtPassword   = new System.Windows.Forms.TextBox();
            this.lblKonfirmasi = new System.Windows.Forms.Label();
            this.txtKonfirmasi = new System.Windows.Forms.TextBox();
            this.lblPesan      = new System.Windows.Forms.Label();
            this.btnDaftar     = new System.Windows.Forms.Button();
            this.btnKembali    = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Dock   = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 130;

            // picLogo
            this.picLogo.Location = new System.Drawing.Point(175, 14);
            this.picLogo.Name     = "picLogo";
            this.picLogo.Size     = new System.Drawing.Size(60, 60);
            this.picLogo.TabStop  = false;

            // lblTitle
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(10, 80);
            this.lblTitle.Size      = new System.Drawing.Size(400, 28);
            this.lblTitle.Text      = "Daftar Akun Baru";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSub
            this.lblSub.AutoSize  = false;
            this.lblSub.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.Location  = new System.Drawing.Point(10, 108);
            this.lblSub.Size      = new System.Drawing.Size(400, 18);
            this.lblSub.Text      = "Isi data berikut untuk membuat akun pengguna";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlForm ──────────────────────────────────────────
            this.pnlForm.Controls.Add(this.lblNama);
            this.pnlForm.Controls.Add(this.txtNama);
            this.pnlForm.Controls.Add(this.lblUsername);
            this.pnlForm.Controls.Add(this.txtUsername);
            this.pnlForm.Controls.Add(this.lblPassword);
            this.pnlForm.Controls.Add(this.txtPassword);
            this.pnlForm.Controls.Add(this.lblKonfirmasi);
            this.pnlForm.Controls.Add(this.txtKonfirmasi);
            this.pnlForm.Controls.Add(this.lblPesan);
            this.pnlForm.Controls.Add(this.btnDaftar);
            this.pnlForm.Controls.Add(this.btnKembali);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;

            // lblNama
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(40, 20);
            this.lblNama.Text     = "Nama Lengkap";
            this.lblNama.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtNama
            this.txtNama.Location = new System.Drawing.Point(40, 40);
            this.txtNama.Name     = "txtNama";
            this.txtNama.Size     = new System.Drawing.Size(340, 26);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 80);
            this.lblUsername.Text     = "Username  (min. 4 karakter)";
            this.lblUsername.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(40, 100);
            this.txtUsername.Name     = "txtUsername";
            this.txtUsername.Size     = new System.Drawing.Size(340, 26);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(40, 140);
            this.lblPassword.Text     = "Password  (min. 6 karakter)";
            this.lblPassword.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtPassword
            this.txtPassword.Location     = new System.Drawing.Point(40, 160);
            this.txtPassword.Name         = "txtPassword";
            this.txtPassword.Size         = new System.Drawing.Size(340, 26);
            this.txtPassword.PasswordChar = '*';

            // lblKonfirmasi
            this.lblKonfirmasi.AutoSize = true;
            this.lblKonfirmasi.Location = new System.Drawing.Point(40, 200);
            this.lblKonfirmasi.Text     = "Konfirmasi Password";
            this.lblKonfirmasi.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtKonfirmasi
            this.txtKonfirmasi.Location     = new System.Drawing.Point(40, 220);
            this.txtKonfirmasi.Name         = "txtKonfirmasi";
            this.txtKonfirmasi.Size         = new System.Drawing.Size(340, 26);
            this.txtKonfirmasi.PasswordChar = '*';

            // lblPesan
            this.lblPesan.AutoSize  = false;
            this.lblPesan.Location  = new System.Drawing.Point(40, 258);
            this.lblPesan.Size      = new System.Drawing.Size(340, 22);
            this.lblPesan.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPesan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPesan.Visible   = false;
            this.lblPesan.Text      = "";

            // btnDaftar
            this.btnDaftar.Location  = new System.Drawing.Point(40, 288);
            this.btnDaftar.Name      = "btnDaftar";
            this.btnDaftar.Size      = new System.Drawing.Size(340, 42);
            this.btnDaftar.Text      = "📝   DAFTAR SEKARANG";
            this.btnDaftar.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDaftar.Click    += new System.EventHandler(this.btnDaftar_Click);

            // btnKembali
            this.btnKembali.Location  = new System.Drawing.Point(40, 340);
            this.btnKembali.Name      = "btnKembali";
            this.btnKembali.Size      = new System.Drawing.Size(340, 30);
            this.btnKembali.Text      = "← Kembali ke Login";
            this.btnKembali.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKembali.Click    += new System.EventHandler(this.btnKembali_Click);

            // ── FormRegister ─────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(420, 510);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox    = false;
            this.Name           = "FormRegister";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "Daftar Akun — ReservasiFutsal02";
            this.Load          += new System.EventHandler(this.FormRegister_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel      pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblSub;
        private System.Windows.Forms.Panel      pnlForm;
        private System.Windows.Forms.Label      lblNama;
        private System.Windows.Forms.TextBox    txtNama;
        private System.Windows.Forms.Label      lblUsername;
        private System.Windows.Forms.TextBox    txtUsername;
        private System.Windows.Forms.Label      lblPassword;
        private System.Windows.Forms.TextBox    txtPassword;
        private System.Windows.Forms.Label      lblKonfirmasi;
        private System.Windows.Forms.TextBox    txtKonfirmasi;
        private System.Windows.Forms.Label      lblPesan;
        private System.Windows.Forms.Button     btnDaftar;
        private System.Windows.Forms.Button     btnKembali;
    }
}
