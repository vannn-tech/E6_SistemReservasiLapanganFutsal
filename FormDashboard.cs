using System;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
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
