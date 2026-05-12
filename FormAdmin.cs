using System;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  FormAdmin — Dashboard utama Administrator
    //  Berisi sidebar navigasi dan Panel untuk setiap sub-form
    //  (Lapangan, Jadwal, Reservasi)
    // ─────────────────────────────────────────────────────────────
    public partial class FormAdmin : Form // Form utama untuk role Admin, berisi sidebar navigasi dan panel konten untuk sub-form manajemen
    {
        public FormAdmin() // constructor untuk inisialisasi komponen form
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            LogoHelper.ApplyLogo(picSidebarLogo, 48);

            // Warna sidebar & header
            pnlSidebar.BackColor     = UITheme.BgSidebar;
            pnlTopBar.BackColor      = UITheme.BgPanel;
            pnlContent.BackColor     = UITheme.BgDark;

            // Label warna
            lblAppTitle.ForeColor    = UITheme.TextPrimary;
            lblAppSub.ForeColor      = UITheme.TextSecondary;
            lblNavMain.ForeColor     = UITheme.TextMuted;
            lblNavData.ForeColor     = UITheme.TextMuted;
            lblPageTitle.ForeColor   = UITheme.TextPrimary;
            lblAdminName.ForeColor   = UITheme.TextPrimary;
            lblAdminRole.ForeColor   = UITheme.TextSecondary;
            lblDateTime.ForeColor    = UITheme.TextSecondary;

            // Style tombol sidebar
            StyleNavButton(btnNavLapangan);
            StyleNavButton(btnNavJadwal);
            StyleNavButton(btnNavReservasi);
            UITheme.StyleButtonDanger(btnLogout);
            UITheme.StyleButtonSecondary(btnNavLapangan);
            UITheme.StyleButtonSecondary(btnNavJadwal);
            UITheme.StyleButtonSecondary(btnNavReservasi);

            // Timer update jam
            timer1.Interval = 1000;
            timer1.Tick    += Timer1_Tick;
            timer1.Start();
            UpdateDateTime();

            // Buka form Lapangan secara default
            BukaSubForm(new FormLapangan());
            SetActiveNav(btnNavLapangan, "🏟️  Manajemen Lapangan");
        }

        // ── Navigasi ke sub-form ──────────────────────────────────
        private void BukaSubForm(Form subForm)
        {
            // Tutup form yang ada di panel
            foreach (Control ctrl in pnlContent.Controls)
            {
                if (ctrl is Form f) f.Close();
            }
            pnlContent.Controls.Clear();

            subForm.TopLevel    = false;
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.Dock        = DockStyle.Fill;
            pnlContent.Controls.Add(subForm);
            subForm.Show();
        }

        private void SetActiveNav(Button activeBtn, string pageTitle)
        {
            Button[] navButtons = { btnNavLapangan, btnNavJadwal, btnNavReservasi };
            foreach (var btn in navButtons)
            {
                btn.BackColor   = UITheme.BgSidebar;
                btn.ForeColor   = UITheme.TextSecondary;
                btn.FlatAppearance.BorderSize = 0;
            }
            activeBtn.BackColor   = UITheme.BgCard;
            activeBtn.ForeColor   = UITheme.AccentGreen;
            lblPageTitle.Text     = pageTitle;
        }

        private void StyleNavButton(Button btn)
        {
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign   = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding     = new Padding(10, 0, 0, 0);
            btn.Cursor      = Cursors.Hand;
        }

        // ── Click handlers navigasi ───────────────────────────────
        private void btnNavLapangan_Click(object sender, EventArgs e)
        {
            BukaSubForm(new FormLapangan());
            SetActiveNav(btnNavLapangan, "🏟️  Manajemen Lapangan");
        }

        private void btnNavJadwal_Click(object sender, EventArgs e)
        {
            BukaSubForm(new FormJadwal());
            SetActiveNav(btnNavJadwal, "📅  Manajemen Jadwal");
        }

        private void btnNavReservasi_Click(object sender, EventArgs e)
        {
            BukaSubForm(new FormReservasi());
            SetActiveNav(btnNavReservasi, "📋  Manajemen Reservasi");
        }

        // ── Logout ────────────────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Yakin ingin logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                timer1.Stop();
                FormDashboard fd = new FormDashboard();
                fd.Show();
                this.Close();
            }
        }

        // ── Timer — update jam di top bar ─────────────────────────
        private void Timer1_Tick(object sender, EventArgs e) => UpdateDateTime();

        private void UpdateDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMM yyyy   HH:mm:ss",
                new System.Globalization.CultureInfo("id-ID"));
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
