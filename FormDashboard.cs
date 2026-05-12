using System;
using System.Windows.Forms;

namespace ReservasiFutsal02 // inisialisasi namespace sesuai nama proyek 
{
    public partial class FormDashboard : Form // Form utama saat aplikasi dijalankan, berisi tombol Login & Register
    {
        public FormDashboard()  // constructor untuk inisialisasi komponen form
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e) // event handler untuk load form, mengatur tema dan gaya visual
        {
            UITheme.ApplyForm(this);
            LogoHelper.ApplyLogo(picLogo, 130);
            UITheme.StyleButtonPrimary(btnLogin);
            UITheme.StyleButtonOrange(btnRegister);

            lblProjectName.ForeColor = UITheme.TextPrimary;
            lblTagline.ForeColor     = UITheme.TextSecondary;
            lblVersion.ForeColor     = UITheme.TextMuted;
            pnlHeader.BackColor      = UITheme.BgPanel;
            pnlButtons.BackColor     = UITheme.BgDark;
            pnlInfo.BackColor        = UITheme.BgCard;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLogin fLogin = new FormLogin();
            fLogin.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister fReg = new FormRegister();
            fReg.ShowDialog();
        }
    }
}
