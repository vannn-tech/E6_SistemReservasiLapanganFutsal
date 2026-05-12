using System;
using System.Drawing;
using System.IO;

namespace ReservasiFutsal02
{
    // Menyimpan logo sebagai resource bawaan (byte array)
    // agar tidak perlu file eksternal
    internal static class LogoHelper
    {
        private static Image _cachedLogo;

        public static Image GetLogo()
        {
            if (_cachedLogo != null) return _cachedLogo;
            try
            {
                byte[] bytes = GetLogoBytes();
                using (var ms = new MemoryStream(bytes))
                    _cachedLogo = Image.FromStream(ms);
            }
            catch { /* gagal load logo — tidak apa-apa */ }
            return _cachedLogo;
        }

        // Pasang logo ke PictureBox
        public static void ApplyLogo(System.Windows.Forms.PictureBox pb, int size = 80)
        {
            var img = GetLogo();
            if (img == null) return;
            pb.Image    = img;
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pb.Width    = size;
            pb.Height   = size;
        }

        private static byte[] GetLogoBytes()
        {
            // Logo PNG di-embed sebagai byte array
            // (di-generate dari file LOGO_Sistem_Reservasi_Lapangan_Futsal.png)
            // Untuk project nyata: ganti dengan Properties.Resources.Logo
            // atau gunakan: return Properties.Resources.logo_bytes;
            //
            // Karena ukuran byte array sangat besar, simpan di file terpisah:
            return LogoBytes.Data;
        }
    }
}
