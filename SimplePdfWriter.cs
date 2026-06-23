using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  SimplePdfWriter — generator PDF sederhana, TANPA dependency
    //  pihak ketiga (tidak butuh NuGet/iTextSharp/dll).
    //
    //  Dipakai oleh FormLaporanReservasi untuk memenuhi Tujuan
    //  Praktikum Modul 13 poin 2: "Meng-export data ke format
    //  CSV dan PDF" — supaya tombol "Export PDF" tetap berfungsi
    //  walau Crystal Reports belum ter-install di komputer pengguna.
    //
    //  Cara kerja: PDF sebenarnya hanyalah teks terstruktur (bukan
    //  format biner rahasia), sehingga sebuah tabel sederhana satu
    //  halaman bisa ditulis langsung sebagai PDF versi 1.4 dengan
    //  font dasar Helvetica (font standar yang WAJIB didukung semua
    //  PDF reader, tidak perlu di-embed).
    //
    //  Keterbatasan (sengaja disederhanakan): satu halaman, font
    //  tetap, tanpa word-wrap otomatis. Untuk laporan dengan baris
    //  sangat banyak, gunakan Crystal Report (lihat FormCetakLaporan.cs)
    //  atau perbesar PageHeight di bawah.
    // ─────────────────────────────────────────────────────────────
    internal static class SimplePdfWriter
    {
        public static void WriteTable(string path, string title, string[] headers, List<string[]> rows)
        {
            const float PageWidth  = 595f;  // A4 dalam point (72 dpi)
            const float PageHeight = 842f;
            const float MarginLeft = 40f;
            float y = PageHeight - 50f;

            var content = new StringBuilder();
            content.AppendLine("BT");
            content.AppendLine("/F1 14 Tf");
            content.AppendLine($"{MarginLeft} {y} Td");
            content.AppendLine($"({Escape(title)}) Tj");
            content.AppendLine("ET");
            y -= 26;

            // Header kolom
            content.AppendLine("BT");
            content.AppendLine("/F1 9 Tf");
            content.AppendLine($"{MarginLeft} {y} Td");
            content.AppendLine($"({Escape(string.Join("   |   ", headers))}) Tj");
            content.AppendLine("ET");
            y -= 6;

            var lineOps = new StringBuilder();
            lineOps.AppendLine($"{MarginLeft} {y} m {PageWidth - MarginLeft} {y} l S");
            y -= 14;

            content.AppendLine("BT");
            content.AppendLine("/F1 8.5 Tf");
            content.AppendLine($"1 0 0 1 {MarginLeft} {y} Tm");
            bool first = true;
            foreach (var row in rows)
            {
                if (y < 40)
                {
                    content.AppendLine($"1 0 0 1 {MarginLeft} {y} Tm");
                    content.AppendLine("(... data terpotong, gunakan Crystal Report untuk laporan panjang ...) Tj");
                    break;
                }
                if (!first)
                    content.AppendLine($"1 0 0 1 {MarginLeft} {y} Tm");
                first = false;
                content.AppendLine($"({Escape(string.Join("   |   ", row))}) Tj");
                y -= 14;
            }
            content.AppendLine("ET");

            string streamText = content.ToString() + lineOps.ToString();
            byte[] streamBytes = Encoding.ASCII.GetBytes(streamText);

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var w = new BinaryWriter(fs))
            {
                var offsets = new List<int>();
                int pos = 0;

                void WriteRaw(string s)
                {
                    byte[] b = Encoding.ASCII.GetBytes(s);
                    w.Write(b);
                    pos += b.Length;
                }

                WriteRaw("%PDF-1.4\n");

                // 1: Catalog
                offsets.Add(pos);
                WriteRaw("1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");

                // 2: Pages
                offsets.Add(pos);
                WriteRaw("2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");

                // 3: Page
                offsets.Add(pos);
                WriteRaw($"3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {PageWidth} {PageHeight}] " +
                         "/Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>\nendobj\n");

                // 4: Content stream
                offsets.Add(pos);
                WriteRaw($"4 0 obj\n<< /Length {streamBytes.Length} >>\nstream\n");
                w.Write(streamBytes);
                pos += streamBytes.Length;
                WriteRaw("\nendstream\nendobj\n");

                // 5: Font
                offsets.Add(pos);
                WriteRaw("5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n");

                int xrefStart = pos;
                WriteRaw($"xref\n0 {offsets.Count + 1}\n0000000000 65535 f \n");
                foreach (int off in offsets)
                    WriteRaw(off.ToString("D10") + " 00000 n \n");

                WriteRaw($"trailer\n<< /Size {offsets.Count + 1} /Root 1 0 R >>\nstartxref\n{xrefStart}\n%%EOF");
            }
        }

        private static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
        }
    }
}
