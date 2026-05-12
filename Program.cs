using System;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Aplikasi dimulai dari FormDashboard (halaman awal pilihan login/register)
            Application.Run(new FormDashboard());
        }
    }
}
