namespace ReservasiFutsal02
{
    partial class FormUser
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
            this.pnlHeader             = new System.Windows.Forms.Panel();
            this.picLogoHeader         = new System.Windows.Forms.PictureBox();
            this.lblSambutan           = new System.Windows.Forms.Label();
            this.btnRefresh            = new System.Windows.Forms.Button();
            this.btnLogout             = new System.Windows.Forms.Button();

            this.tabControl            = new System.Windows.Forms.TabControl();
            this.tabJadwal             = new System.Windows.Forms.TabPage();
            this.tabRiwayat            = new System.Windows.Forms.TabPage();

            // Tab Jadwal
            this.lblFilterLapangan     = new System.Windows.Forms.Label();
            this.cmbFilterLapangan     = new System.Windows.Forms.ComboBox();
            this.lblInfoJadwal         = new System.Windows.Forms.Label();
            this.dgvJadwalTersedia     = new System.Windows.Forms.DataGridView();
            this.btnPesan              = new System.Windows.Forms.Button();

            // Tab Riwayat
            this.lblRiwayat            = new System.Windows.Forms.Label();
            this.dgvRiwayat            = new System.Windows.Forms.DataGridView();
            this.btnBatalkan           = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoHeader)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabJadwal.SuspendLayout();
            this.tabRiwayat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwalTersedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────
            this.pnlHeader.Controls.Add(this.picLogoHeader);
            this.pnlHeader.Controls.Add(this.lblSambutan);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Dock   = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 58;

            // picLogoHeader
            this.picLogoHeader.Location = new System.Drawing.Point(12, 9);
            this.picLogoHeader.Name     = "picLogoHeader";
            this.picLogoHeader.Size     = new System.Drawing.Size(40, 40);
            this.picLogoHeader.TabStop  = false;

            // lblSambutan
            this.lblSambutan.AutoSize  = false;
            this.lblSambutan.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSambutan.Location  = new System.Drawing.Point(60, 15);
            this.lblSambutan.Size      = new System.Drawing.Size(480, 28);
            this.lblSambutan.Text      = "Selamat datang!";

            // btnRefresh
            this.btnRefresh.Location  = new System.Drawing.Point(590, 14);
            this.btnRefresh.Name      = "btnRefresh";
            this.btnRefresh.Size      = new System.Drawing.Size(85, 30);
            this.btnRefresh.Text      = "🔄  Refresh";
            this.btnRefresh.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Click    += new System.EventHandler(this.btnRefresh_Click);

            // btnLogout
            this.btnLogout.Location  = new System.Drawing.Point(685, 14);
            this.btnLogout.Name      = "btnLogout";
            this.btnLogout.Size      = new System.Drawing.Size(85, 30);
            this.btnLogout.Text      = "⏏  Logout";
            this.btnLogout.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Click    += new System.EventHandler(this.btnLogout_Click);

            // ── TabControl ───────────────────────────────────────
            this.tabControl.Location = new System.Drawing.Point(10, 68);
            this.tabControl.Name     = "tabControl";
            this.tabControl.Size     = new System.Drawing.Size(780, 420);
            this.tabControl.Controls.Add(this.tabJadwal);
            this.tabControl.Controls.Add(this.tabRiwayat);

            // ── TAB 1: Jadwal Tersedia ────────────────────────────
            this.tabJadwal.Name    = "tabJadwal";
            this.tabJadwal.Text    = "  📅  Jadwal Tersedia  ";
            this.tabJadwal.Padding = new System.Windows.Forms.Padding(8);
            this.tabJadwal.Controls.Add(this.lblFilterLapangan);
            this.tabJadwal.Controls.Add(this.cmbFilterLapangan);
            this.tabJadwal.Controls.Add(this.lblInfoJadwal);
            this.tabJadwal.Controls.Add(this.dgvJadwalTersedia);
            this.tabJadwal.Controls.Add(this.btnPesan);

            // lblFilterLapangan
            this.lblFilterLapangan.AutoSize = true;
            this.lblFilterLapangan.Location = new System.Drawing.Point(10, 14);
            this.lblFilterLapangan.Text     = "Filter Lapangan :";
            this.lblFilterLapangan.Font     = new System.Drawing.Font("Segoe UI", 9F);

            // cmbFilterLapangan
            this.cmbFilterLapangan.DropDownStyle        = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLapangan.Location              = new System.Drawing.Point(130, 11);
            this.cmbFilterLapangan.Name                  = "cmbFilterLapangan";
            this.cmbFilterLapangan.Size                  = new System.Drawing.Size(210, 24);
            this.cmbFilterLapangan.Font                  = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterLapangan.SelectedIndexChanged += new System.EventHandler(this.cmbFilterLapangan_SelectedIndexChanged);

            // lblInfoJadwal
            this.lblInfoJadwal.AutoSize  = false;
            this.lblInfoJadwal.Location  = new System.Drawing.Point(360, 14);
            this.lblInfoJadwal.Size      = new System.Drawing.Size(390, 20);
            this.lblInfoJadwal.Text      = "Jadwal tersedia: 0 slot";
            this.lblInfoJadwal.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoJadwal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // dgvJadwalTersedia
            this.dgvJadwalTersedia.Location            = new System.Drawing.Point(10, 42);
            this.dgvJadwalTersedia.Name                = "dgvJadwalTersedia";
            this.dgvJadwalTersedia.Size                = new System.Drawing.Size(748, 310);
            this.dgvJadwalTersedia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJadwalTersedia.ReadOnly            = true;
            this.dgvJadwalTersedia.SelectionMode       = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJadwalTersedia.AllowUserToAddRows  = false;

            // btnPesan
            this.btnPesan.Location  = new System.Drawing.Point(10, 360);
            this.btnPesan.Name      = "btnPesan";
            this.btnPesan.Size      = new System.Drawing.Size(180, 38);
            this.btnPesan.Text      = "📅  PESAN JADWAL INI";
            this.btnPesan.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPesan.Click    += new System.EventHandler(this.btnPesan_Click);

            // ── TAB 2: Riwayat Saya ──────────────────────────────
            this.tabRiwayat.Name    = "tabRiwayat";
            this.tabRiwayat.Text    = "  📋  Riwayat Reservasi Saya  ";
            this.tabRiwayat.Padding = new System.Windows.Forms.Padding(8);
            this.tabRiwayat.Controls.Add(this.lblRiwayat);
            this.tabRiwayat.Controls.Add(this.dgvRiwayat);
            this.tabRiwayat.Controls.Add(this.btnBatalkan);

            // lblRiwayat
            this.lblRiwayat.AutoSize = true;
            this.lblRiwayat.Location = new System.Drawing.Point(10, 12);
            this.lblRiwayat.Text     = "Daftar seluruh reservasi yang pernah Anda buat:";
            this.lblRiwayat.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);

            // dgvRiwayat
            this.dgvRiwayat.Location            = new System.Drawing.Point(10, 36);
            this.dgvRiwayat.Name                = "dgvRiwayat";
            this.dgvRiwayat.Size                = new System.Drawing.Size(748, 316);
            this.dgvRiwayat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRiwayat.ReadOnly            = true;
            this.dgvRiwayat.SelectionMode       = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRiwayat.AllowUserToAddRows  = false;

            // btnBatalkan
            this.btnBatalkan.Location  = new System.Drawing.Point(10, 360);
            this.btnBatalkan.Name      = "btnBatalkan";
            this.btnBatalkan.Size      = new System.Drawing.Size(180, 38);
            this.btnBatalkan.Text      = "❌  BATALKAN RESERVASI";
            this.btnBatalkan.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBatalkan.Click    += new System.EventHandler(this.btnBatalkan_Click);

            // ── FormUser ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox    = false;
            this.Name           = "FormUser";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "Panel User — ReservasiFutsal02";
            this.Load          += new System.EventHandler(this.FormUser_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogoHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwalTersedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.tabJadwal.ResumeLayout(false);
            this.tabJadwal.PerformLayout();
            this.tabRiwayat.ResumeLayout(false);
            this.tabRiwayat.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel          pnlHeader;
        private System.Windows.Forms.PictureBox     picLogoHeader;
        private System.Windows.Forms.Label          lblSambutan;
        private System.Windows.Forms.Button         btnRefresh;
        private System.Windows.Forms.Button         btnLogout;
        private System.Windows.Forms.TabControl     tabControl;
        private System.Windows.Forms.TabPage        tabJadwal;
        private System.Windows.Forms.TabPage        tabRiwayat;
        private System.Windows.Forms.Label          lblFilterLapangan;
        private System.Windows.Forms.ComboBox       cmbFilterLapangan;
        private System.Windows.Forms.Label          lblInfoJadwal;
        private System.Windows.Forms.DataGridView   dgvJadwalTersedia;
        private System.Windows.Forms.Button         btnPesan;
        private System.Windows.Forms.Label          lblRiwayat;
        private System.Windows.Forms.DataGridView   dgvRiwayat;
        private System.Windows.Forms.Button         btnBatalkan;
    }
}
