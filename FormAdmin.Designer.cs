namespace ReservasiFutsal02
{
    partial class FormAdmin
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
            this.components      = new System.ComponentModel.Container();
            this.timer1          = new System.Windows.Forms.Timer(this.components);
            this.pnlSidebar      = new System.Windows.Forms.Panel();
            this.picSidebarLogo  = new System.Windows.Forms.PictureBox();
            this.lblAppTitle     = new System.Windows.Forms.Label();
            this.lblAppSub       = new System.Windows.Forms.Label();
            this.sep1            = new System.Windows.Forms.Panel();
            this.lblNavMain      = new System.Windows.Forms.Label();
            this.btnNavLapangan  = new System.Windows.Forms.Button();
            this.btnNavJadwal    = new System.Windows.Forms.Button();
            this.btnNavReservasi = new System.Windows.Forms.Button();
            this.sep2            = new System.Windows.Forms.Panel();
            this.lblNavData      = new System.Windows.Forms.Label();
            this.pnlSidebarUser  = new System.Windows.Forms.Panel();
            this.lblAdminName    = new System.Windows.Forms.Label();
            this.lblAdminRole    = new System.Windows.Forms.Label();
            this.btnLogout       = new System.Windows.Forms.Button();
            this.pnlTopBar       = new System.Windows.Forms.Panel();
            this.lblPageTitle    = new System.Windows.Forms.Label();
            this.lblDateTime     = new System.Windows.Forms.Label();
            this.pnlContent      = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSidebarLogo)).BeginInit();
            this.pnlTopBar.SuspendLayout();
            this.pnlSidebarUser.SuspendLayout();
            this.SuspendLayout();

            // ── pnlSidebar ───────────────────────────────────────
            this.pnlSidebar.Controls.Add(this.picSidebarLogo);
            this.pnlSidebar.Controls.Add(this.lblAppTitle);
            this.pnlSidebar.Controls.Add(this.lblAppSub);
            this.pnlSidebar.Controls.Add(this.sep1);
            this.pnlSidebar.Controls.Add(this.lblNavMain);
            this.pnlSidebar.Controls.Add(this.btnNavLapangan);
            this.pnlSidebar.Controls.Add(this.btnNavJadwal);
            this.pnlSidebar.Controls.Add(this.btnNavReservasi);
            this.pnlSidebar.Controls.Add(this.sep2);
            this.pnlSidebar.Controls.Add(this.pnlSidebarUser);
            this.pnlSidebar.Dock     = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width    = 220;
            this.pnlSidebar.Name     = "pnlSidebar";

            // picSidebarLogo
            this.picSidebarLogo.Location = new System.Drawing.Point(86, 18);
            this.picSidebarLogo.Name     = "picSidebarLogo";
            this.picSidebarLogo.Size     = new System.Drawing.Size(48, 48);
            this.picSidebarLogo.TabStop  = false;

            // lblAppTitle
            this.lblAppTitle.AutoSize  = false;
            this.lblAppTitle.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.Location  = new System.Drawing.Point(10, 72);
            this.lblAppTitle.Size      = new System.Drawing.Size(200, 24);
            this.lblAppTitle.Text      = "ReservasiFutsal02";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblAppSub
            this.lblAppSub.AutoSize  = false;
            this.lblAppSub.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAppSub.Location  = new System.Drawing.Point(10, 94);
            this.lblAppSub.Size      = new System.Drawing.Size(200, 20);
            this.lblAppSub.Text      = "Panel Administrator";
            this.lblAppSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // sep1
            this.sep1.Location  = new System.Drawing.Point(0, 120);
            this.sep1.Size      = new System.Drawing.Size(220, 1);
            this.sep1.BackColor = System.Drawing.Color.FromArgb(48, 54, 61);

            // lblNavMain
            this.lblNavMain.AutoSize = true;
            this.lblNavMain.Font     = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavMain.Location = new System.Drawing.Point(14, 130);
            this.lblNavMain.Text     = "MENU UTAMA";

            // btnNavLapangan
            this.btnNavLapangan.Location  = new System.Drawing.Point(0, 152);
            this.btnNavLapangan.Name      = "btnNavLapangan";
            this.btnNavLapangan.Size      = new System.Drawing.Size(220, 42);
            this.btnNavLapangan.Text      = "  🏟️   Manajemen Lapangan";
            this.btnNavLapangan.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavLapangan.Click    += new System.EventHandler(this.btnNavLapangan_Click);

            // btnNavJadwal
            this.btnNavJadwal.Location  = new System.Drawing.Point(0, 194);
            this.btnNavJadwal.Name      = "btnNavJadwal";
            this.btnNavJadwal.Size      = new System.Drawing.Size(220, 42);
            this.btnNavJadwal.Text      = "  📅   Manajemen Jadwal";
            this.btnNavJadwal.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavJadwal.Click    += new System.EventHandler(this.btnNavJadwal_Click);

            // btnNavReservasi
            this.btnNavReservasi.Location  = new System.Drawing.Point(0, 236);
            this.btnNavReservasi.Name      = "btnNavReservasi";
            this.btnNavReservasi.Size      = new System.Drawing.Size(220, 42);
            this.btnNavReservasi.Text      = "  📋   Manajemen Reservasi";
            this.btnNavReservasi.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavReservasi.Click    += new System.EventHandler(this.btnNavReservasi_Click);

            // sep2
            this.sep2.Location  = new System.Drawing.Point(0, 285);
            this.sep2.Size      = new System.Drawing.Size(220, 1);
            this.sep2.BackColor = System.Drawing.Color.FromArgb(48, 54, 61);

            // pnlSidebarUser (bawah sidebar)
            this.pnlSidebarUser.Controls.Add(this.lblAdminName);
            this.pnlSidebarUser.Controls.Add(this.lblAdminRole);
            this.pnlSidebarUser.Controls.Add(this.btnLogout);
            this.pnlSidebarUser.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarUser.Height   = 70;

            // lblAdminName
            this.lblAdminName.AutoSize = true;
            this.lblAdminName.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminName.Location = new System.Drawing.Point(14, 10);
            this.lblAdminName.Text     = "Administrator";

            // lblAdminRole
            this.lblAdminRole.AutoSize = true;
            this.lblAdminRole.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAdminRole.Location = new System.Drawing.Point(14, 30);
            this.lblAdminRole.Text     = "Role: Admin";

            // btnLogout
            this.btnLogout.Location  = new System.Drawing.Point(130, 18);
            this.btnLogout.Name      = "btnLogout";
            this.btnLogout.Size      = new System.Drawing.Size(78, 32);
            this.btnLogout.Text      = "⏏ Logout";
            this.btnLogout.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Click    += new System.EventHandler(this.btnLogout_Click);

            // ── pnlTopBar ────────────────────────────────────────
            this.pnlTopBar.Controls.Add(this.lblPageTitle);
            this.pnlTopBar.Controls.Add(this.lblDateTime);
            this.pnlTopBar.Dock   = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 46;

            // lblPageTitle
            this.lblPageTitle.AutoSize  = false;
            this.lblPageTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location  = new System.Drawing.Point(12, 10);
            this.lblPageTitle.Size      = new System.Drawing.Size(400, 26);
            this.lblPageTitle.Text      = "Dashboard Admin";

            // lblDateTime
            this.lblDateTime.AutoSize  = false;
            this.lblDateTime.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDateTime.Location  = new System.Drawing.Point(420, 14);
            this.lblDateTime.Size      = new System.Drawing.Size(340, 18);
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDateTime.Text      = "";

            // ── pnlContent ───────────────────────────────────────
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";

            // ── FormAdmin ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(1020, 620);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox    = false;
            this.Name           = "FormAdmin";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "Panel Admin — ReservasiFutsal02";
            this.Load          += new System.EventHandler(this.FormAdmin_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSidebarLogo)).EndInit();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlSidebarUser.ResumeLayout(false);
            this.pnlSidebarUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer       timer1;
        private System.Windows.Forms.Panel       pnlSidebar;
        private System.Windows.Forms.PictureBox  picSidebarLogo;
        private System.Windows.Forms.Label       lblAppTitle;
        private System.Windows.Forms.Label       lblAppSub;
        private System.Windows.Forms.Panel       sep1;
        private System.Windows.Forms.Label       lblNavMain;
        private System.Windows.Forms.Button      btnNavLapangan;
        private System.Windows.Forms.Button      btnNavJadwal;
        private System.Windows.Forms.Button      btnNavReservasi;
        private System.Windows.Forms.Panel       sep2;
        private System.Windows.Forms.Label       lblNavData;
        private System.Windows.Forms.Panel       pnlSidebarUser;
        private System.Windows.Forms.Label       lblAdminName;
        private System.Windows.Forms.Label       lblAdminRole;
        private System.Windows.Forms.Button      btnLogout;
        private System.Windows.Forms.Panel       pnlTopBar;
        private System.Windows.Forms.Label       lblPageTitle;
        private System.Windows.Forms.Label       lblDateTime;
        private System.Windows.Forms.Panel       pnlContent;
    }
}
