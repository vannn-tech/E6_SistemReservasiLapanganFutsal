using System.Drawing;
using System.Windows.Forms;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  UITheme — palet warna & helper styling terpusat
    //  Semua form menggunakan konstanta ini agar tampilan konsisten
    // ─────────────────────────────────────────────────────────────
    internal static class UITheme
    {
        // ── Warna Utama ──────────────────────────────────────────
        public static readonly Color BgDark        = Color.FromArgb(18, 18, 18);
        public static readonly Color BgPanel       = Color.FromArgb(28, 28, 30);
        public static readonly Color BgCard        = Color.FromArgb(38, 38, 40);
        public static readonly Color BgInput       = Color.FromArgb(48, 48, 52);
        public static readonly Color BgSidebar     = Color.FromArgb(22, 22, 24);

        public static readonly Color AccentGreen   = Color.FromArgb(34, 197, 94);
        public static readonly Color AccentOrange  = Color.FromArgb(249, 115, 22);
        public static readonly Color AccentAmber   = Color.FromArgb(251, 191, 36);
        public static readonly Color AccentBlue    = Color.FromArgb(59, 130, 246);
        public static readonly Color AccentRed     = Color.FromArgb(239, 68, 68);

        public static readonly Color TextPrimary   = Color.FromArgb(240, 246, 252);
        public static readonly Color TextSecondary = Color.FromArgb(139, 148, 158);
        public static readonly Color TextMuted     = Color.FromArgb(72, 79, 88);

        public static readonly Color BorderColor   = Color.FromArgb(48, 54, 61);

        // ── Font ─────────────────────────────────────────────────
        public static readonly Font FontTitle  = new Font("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font FontHeader = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font FontBody   = new Font("Segoe UI", 9.5F);
        public static readonly Font FontSmall  = new Font("Segoe UI", 8.5F);
        public static readonly Font FontBold   = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font FontMono   = new Font("Consolas", 9F);

        // ── Terapkan tema ke Form ─────────────────────────────────
        public static void ApplyForm(Form form)
        {
            form.BackColor = BgDark;
            form.ForeColor = TextPrimary;
            form.Font      = FontBody;
        }

        // ── Styling tombol aksi utama (hijau) ────────────────────
        public static void StyleButtonPrimary(Button btn)
        {
            btn.BackColor   = AccentGreen;
            btn.ForeColor   = Color.FromArgb(10, 10, 10);
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize      = 0;
            btn.FlatAppearance.MouseOverBackColor  = Color.FromArgb(74, 222, 128);
            btn.FlatAppearance.MouseDownBackColor  = Color.FromArgb(21, 128, 61);
            btn.Font        = FontBold;
            btn.Cursor      = Cursors.Hand;
        }

        // ── Styling tombol sekunder (abu-abu gelap) ──────────────
        public static void StyleButtonSecondary(Button btn)
        {
            btn.BackColor   = BgCard;
            btn.ForeColor   = TextSecondary;
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor         = BorderColor;
            btn.FlatAppearance.BorderSize          = 1;
            btn.FlatAppearance.MouseOverBackColor  = BgInput;
            btn.Font        = FontBody;
            btn.Cursor      = Cursors.Hand;
        }

        // ── Styling tombol bahaya (merah) ─────────────────────────
        public static void StyleButtonDanger(Button btn)
        {
            btn.BackColor   = Color.FromArgb(60, 20, 20);
            btn.ForeColor   = AccentRed;
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor         = Color.FromArgb(100, 30, 30);
            btn.FlatAppearance.BorderSize          = 1;
            btn.FlatAppearance.MouseOverBackColor  = Color.FromArgb(80, 25, 25);
            btn.Font        = FontBold;
            btn.Cursor      = Cursors.Hand;
        }

        // ── Styling tombol orange ─────────────────────────────────
        public static void StyleButtonOrange(Button btn)
        {
            btn.BackColor   = Color.FromArgb(60, 30, 10);
            btn.ForeColor   = AccentOrange;
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor         = Color.FromArgb(100, 55, 15);
            btn.FlatAppearance.BorderSize          = 1;
            btn.FlatAppearance.MouseOverBackColor  = Color.FromArgb(80, 40, 12);
            btn.Font        = FontBold;
            btn.Cursor      = Cursors.Hand;
        }

        // ── Styling TextBox ──────────────────────────────────────
        public static void StyleTextBox(TextBox tb)
        {
            tb.BackColor   = BgInput;
            tb.ForeColor   = TextPrimary;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Font        = FontBody;
        }

        // ── Styling ComboBox ─────────────────────────────────────
        public static void StyleComboBox(ComboBox cb)
        {
            cb.BackColor   = BgInput;
            cb.ForeColor   = TextPrimary;
            cb.FlatStyle   = FlatStyle.Flat;
            cb.Font        = FontBody;
        }

        // ── Styling DataGridView ─────────────────────────────────
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor        = BgPanel;
            dgv.GridColor              = BorderColor;
            dgv.BorderStyle            = BorderStyle.FixedSingle;
            dgv.DefaultCellStyle.BackColor  = BgPanel;
            dgv.DefaultCellStyle.ForeColor  = TextPrimary;
            dgv.DefaultCellStyle.Font       = FontBody;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(34, 60, 34);
            dgv.DefaultCellStyle.SelectionForeColor = AccentGreen;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = BgCard;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible        = false;
            dgv.AllowUserToResizeRows    = false;
        }

        // ── Panel separator horizontal ────────────────────────────
        public static Panel MakeSeparator(int x, int y, int width)
        {
            return new Panel
            {
                Location  = new Point(x, y),
                Size      = new Size(width, 1),
                BackColor = BorderColor
            };
        }
    }
}
