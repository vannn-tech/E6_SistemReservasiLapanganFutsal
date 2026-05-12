namespace ReservasiFutsal02
{
    partial class FormDashboard
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
            this.pnlHeader      = new System.Windows.Forms.Panel();
            this.picLogo        = new System.Windows.Forms.PictureBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblTagline     = new System.Windows.Forms.Label();
            this.pnlButtons     = new System.Windows.Forms.Panel();
            this.btnLogin       = new System.Windows.Forms.Button();
            this.lblOr          = new System.Windows.Forms.Label();
            this.btnRegister    = new System.Windows.Forms.Button();
            this.pnlInfo        = new System.Windows.Forms.Panel();
            this.lblInfo1       = new System.Windows.Forms.Label();
            this.lblInfo2       = new System.Windows.Forms.Label();
            this.lblInfo3       = new System.Windows.Forms.Label();
            this.lblVersion     = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.lblProjectName);
            this.pnlHeader.Controls.Add(this.lblTagline);
            this.pnlHeader.Dock   = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 255;

            // picLogo
            this.picLogo.Location = new System.Drawing.Point(160, 20);
            this.picLogo.Name     = "picLogo";
            this.picLogo.Size     = new System.Drawing.Size(130, 130);
            this.picLogo.TabStop  = false;

            // lblProjectName
            this.lblProjectName.AutoSize  = false;
            this.lblProjectName.Font      = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblProjectName.Location  = new System.Drawing.Point(20, 162);
            this.lblProjectName.Size      = new System.Drawing.Size(410, 46);
            this.lblProjectName.Text      = "ReservasiFutsal02";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblTagline
            this.lblTagline.AutoSize  = false;
            this.lblTagline.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTagline.Location  = new System.Drawing.Point(20, 208);
            this.lblTagline.Size      = new System.Drawing.Size(410, 30);
            this.lblTagline.Text      = "Sistem Reservasi Lapangan Futsal — Visual Studio 2022";
            this.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlButtons ───────────────────────────────────────
            this.pnlButtons.Controls.Add(this.btnLogin);
            this.pnlButtons.Controls.Add(this.lblOr);
            this.pnlButtons.Controls.Add(this.btnRegister);
            this.pnlButtons.Dock    = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Height  = 130;
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(60, 16, 60, 16);

            // btnLogin
            this.btnLogin.Location  = new System.Drawing.Point(60, 18);
            this.btnLogin.Name      = "btnLogin";
            this.btnLogin.Size      = new System.Drawing.Size(330, 44);
            this.btnLogin.Text      = "🔐   MASUK (LOGIN)";
            this.btnLogin.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Click    += new System.EventHandler(this.btnLogin_Click);

            // lblOr
            this.lblOr.AutoSize  = false;
            this.lblOr.Location  = new System.Drawing.Point(60, 66);
            this.lblOr.Size      = new System.Drawing.Size(330, 18);
            this.lblOr.Text      = "— atau —";
            this.lblOr.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblOr.ForeColor = System.Drawing.Color.FromArgb(72, 79, 88);
            this.lblOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnRegister
            this.btnRegister.Location  = new System.Drawing.Point(60, 88);
            this.btnRegister.Name      = "btnRegister";
            this.btnRegister.Size      = new System.Drawing.Size(330, 38);
            this.btnRegister.Text      = "📝   DAFTAR AKUN BARU";
            this.btnRegister.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.Click    += new System.EventHandler(this.btnRegister_Click);

            // ── pnlInfo ──────────────────────────────────────────
            this.pnlInfo.Controls.Add(this.lblInfo1);
            this.pnlInfo.Controls.Add(this.lblInfo2);
            this.pnlInfo.Controls.Add(this.lblInfo3);
            this.pnlInfo.Controls.Add(this.lblVersion);
            this.pnlInfo.Dock    = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(30, 14, 30, 14);

            // lblInfo1
            this.lblInfo1.AutoSize  = false;
            this.lblInfo1.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfo1.ForeColor = System.Drawing.Color.FromArgb(139, 148, 158);
            this.lblInfo1.Location  = new System.Drawing.Point(30, 14);
            this.lblInfo1.Size      = new System.Drawing.Size(390, 22);
            this.lblInfo1.Text      = "✅  Admin: kelola lapangan, jadwal & reservasi";
            this.lblInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblInfo2
            this.lblInfo2.AutoSize  = false;
            this.lblInfo2.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfo2.ForeColor = System.Drawing.Color.FromArgb(139, 148, 158);
            this.lblInfo2.Location  = new System.Drawing.Point(30, 38);
            this.lblInfo2.Size      = new System.Drawing.Size(390, 22);
            this.lblInfo2.Text      = "✅  User: lihat jadwal tersedia & buat reservasi";
            this.lblInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblInfo3
            this.lblInfo3.AutoSize  = false;
            this.lblInfo3.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfo3.ForeColor = System.Drawing.Color.FromArgb(139, 148, 158);
            this.lblInfo3.Location  = new System.Drawing.Point(30, 62);
            this.lblInfo3.Size      = new System.Drawing.Size(390, 22);
            this.lblInfo3.Text      = "✅  Akun demo: admin/admin123 · budi/budi123";
            this.lblInfo3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblVersion
            this.lblVersion.AutoSize  = false;
            this.lblVersion.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.Location  = new System.Drawing.Point(30, 92);
            this.lblVersion.Size      = new System.Drawing.Size(390, 20);
            this.lblVersion.Text      = "ReservasiFutsal02  v1.0  |  .NET Framework 4.8  |  SQL Server";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── FormDashboard ────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(450, 510);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox    = false;
            this.Name           = "FormDashboard";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "ReservasiFutsal02 — Sistem Reservasi Lapangan Futsal";
            this.Load          += new System.EventHandler(this.FormDashboard_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel      pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label      lblProjectName;
        private System.Windows.Forms.Label      lblTagline;
        private System.Windows.Forms.Panel      pnlButtons;
        private System.Windows.Forms.Button     btnLogin;
        private System.Windows.Forms.Label      lblOr;
        private System.Windows.Forms.Button     btnRegister;
        private System.Windows.Forms.Panel      pnlInfo;
        private System.Windows.Forms.Label      lblInfo1;
        private System.Windows.Forms.Label      lblInfo2;
        private System.Windows.Forms.Label      lblInfo3;
        private System.Windows.Forms.Label      lblVersion;
    }
}
