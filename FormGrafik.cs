using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  FormGrafik — Dashboard statistik reservasi (Modul 14).
    //  Dibuka sebagai sub-form di dalam pnlContent FormAdmin, sama
    //  seperti FormLapangan / FormJadwal / FormReservasi — BUKAN
    //  pop-up terpisah — supaya konsisten dengan pola navigasi
    //  sidebar yang sudah ada.
    //
    //  Tiga jenis tampilan grafik:
    //   • Per Lapangan  -> grafik kolom, jumlah reservasi per lapangan
    //   • Per Status    -> grafik pie, distribusi Aktif/Selesai/Dibatalkan
    //   • Per Bulan     -> grafik kolom, tren reservasi sepanjang tahun terpilih
    // ─────────────────────────────────────────────────────────────
    public partial class FormGrafik : Form
    {
        string connectionString = @"Data Source=10.200.161.237\MSSQLSERVER01;Initial Catalog=DBFutsalADO;User ID=sa;Password=jovan1532006";

        private bool isInitializing = false;

        public FormGrafik()
        {
            InitializeComponent();
        }

        private void FormGrafik_Load(object sender, EventArgs e)
        {
            UITheme.ApplyForm(this);
            UITheme.StyleButtonPrimary(btnLoad);
            UITheme.StyleButtonSecondary(btnReset);
            UITheme.StyleComboBox(cmbTampilan);
            UITheme.StyleComboBox(cmbTahun);

            lblTitle.ForeColor = UITheme.TextPrimary;
            pnlTop.BackColor   = UITheme.BgPanel;
            this.BackColor     = UITheme.BgDark;

            isInitializing = true;

            cmbTampilan.Items.Clear();
            cmbTampilan.Items.AddRange(new string[] { "Per Lapangan", "Per Status", "Per Bulan" });
            cmbTampilan.SelectedIndex = 0;

            MuatComboTahun();

            isInitializing = false;

            LoadChart();
        }

        // ── Isi pilihan tahun: "Semua Tahun" + 4 tahun terakhir ──────
        private void MuatComboTahun()
        {
            cmbTahun.Items.Clear();
            cmbTahun.Items.Add("Semua Tahun");
            int tahunIni = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
                cmbTahun.Items.Add((tahunIni - i).ToString());
            cmbTahun.SelectedIndex = 0;
        }

        private int TahunTerpilih()
        {
            if (cmbTahun.SelectedIndex <= 0) return 0; // 0 = semua tahun
            return int.Parse(cmbTahun.Text);
        }

        // ── Render ulang chart sesuai pilihan combo ─────────────────
        private void LoadChart()
        {
            if (isInitializing) return;

            string tampilan = cmbTampilan.Text;

            chartReservasi.Series.Clear();
            chartReservasi.Titles.Clear();
            chartReservasi.Legends.Clear();
            chartReservasi.ChartAreas.Clear();

            ChartArea ca = new ChartArea("MainArea");
            ca.BackColor = Color.Transparent;
            ca.AxisX.LabelStyle.ForeColor = UITheme.TextSecondary;
            ca.AxisY.LabelStyle.ForeColor = UITheme.TextSecondary;
            ca.AxisX.LineColor = UITheme.BorderColor;
            ca.AxisY.LineColor = UITheme.BorderColor;
            ca.AxisX.MajorGrid.LineColor = UITheme.BorderColor;
            ca.AxisY.MajorGrid.LineColor = UITheme.BorderColor;
            chartReservasi.ChartAreas.Add(ca);

            Legend legend = new Legend("MainLegend") { Docking = Docking.Right };
            legend.ForeColor = UITheme.TextSecondary;
            chartReservasi.Legends.Add(legend);

            try
            {
                switch (tampilan)
                {
                    case "Per Status":
                        LoadPerStatus(ca);
                        break;
                    case "Per Bulan":
                        LoadPerBulan(ca);
                        break;
                    default:
                        LoadPerLapangan(ca);
                        break;
                }
            }
            catch (SqlException ex)
            {
                AppLogger.SimpanLog("FormGrafik.LoadChart", ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormGrafik.LoadChart", ex.Message);
                MessageBox.Show("Gagal memuat grafik: " + ex.Message);
            }
        }

        // ── Grafik kolom: jumlah reservasi per lapangan ─────────────
        private void LoadPerLapangan(ChartArea ca)
        {
            ca.AxisX.Title = "Lapangan";
            ca.AxisY.Title = "Jumlah Reservasi";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DashboardPerLapangan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tahun", TahunTerpilih());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            Series s = new Series("Jumlah Reservasi") { ChartType = SeriesChartType.Column };
            s.Color = UITheme.AccentGreen;
            s.IsValueShownAsLabel = true;
            foreach (DataRow row in dt.Rows)
                s.Points.AddXY(row["NamaLapangan"].ToString(), Convert.ToInt32(row["JumlahReservasi"]));

            chartReservasi.Series.Add(s);
            chartReservasi.Titles.Add(new Title("Jumlah Reservasi per Lapangan",
                Docking.Top, new Font("Segoe UI", 12F, FontStyle.Bold), UITheme.TextPrimary));
        }

        // ── Grafik pie: distribusi status reservasi ─────────────────
        private void LoadPerStatus(ChartArea ca)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DashboardPerStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tahun", TahunTerpilih());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            Series s = new Series("Status") { ChartType = SeriesChartType.Pie };
            s.IsValueShownAsLabel = true;
            s.Label = "#VALX (#VALY)";
            foreach (DataRow row in dt.Rows)
                s.Points.AddXY(row["Status"].ToString(), Convert.ToInt32(row["Jumlah"]));

            // Warna konsisten dengan tema status di Form lain
            foreach (DataPoint p in s.Points)
            {
                if (p.AxisLabel == "Aktif")      p.Color = UITheme.AccentGreen;
                else if (p.AxisLabel == "Selesai")   p.Color = UITheme.AccentBlue;
                else if (p.AxisLabel == "Dibatalkan") p.Color = UITheme.AccentRed;
            }

            chartReservasi.Series.Add(s);
            chartReservasi.Titles.Add(new Title("Distribusi Status Reservasi",
                Docking.Top, new Font("Segoe UI", 12F, FontStyle.Bold), UITheme.TextPrimary));
        }

        // ── Grafik kolom: tren reservasi per bulan pada tahun terpilih ──
        private void LoadPerBulan(ChartArea ca)
        {
            int tahun = TahunTerpilih();
            if (tahun == 0) tahun = DateTime.Now.Year; // "Per Bulan" butuh tahun spesifik

            ca.AxisX.Title = "Bulan";
            ca.AxisY.Title = "Jumlah Reservasi";

            string[] namaBulan = { "Jan","Feb","Mar","Apr","Mei","Jun","Jul","Agu","Sep","Okt","Nov","Des" };
            int[] jumlahPerBulan = new int[12];

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DashboardPerBulan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tahun", tahun);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                int bulan = Convert.ToInt32(row["Bulan"]);
                if (bulan >= 1 && bulan <= 12)
                    jumlahPerBulan[bulan - 1] = Convert.ToInt32(row["Jumlah"]);
            }

            Series s = new Series("Reservasi " + tahun) { ChartType = SeriesChartType.Column };
            s.Color = UITheme.AccentOrange;
            s.IsValueShownAsLabel = true;
            for (int i = 0; i < 12; i++)
                s.Points.AddXY(namaBulan[i], jumlahPerBulan[i]);

            chartReservasi.Series.Add(s);
            chartReservasi.Titles.Add(new Title($"Tren Reservasi per Bulan — {tahun}",
                Docking.Top, new Font("Segoe UI", 12F, FontStyle.Bold), UITheme.TextPrimary));
        }

        private void btnLoad_Click(object sender, EventArgs e) => LoadChart();

        private void btnReset_Click(object sender, EventArgs e)
        {
            isInitializing = true;
            cmbTampilan.SelectedIndex = 0;
            cmbTahun.SelectedIndex    = 0;
            isInitializing = false;
            LoadChart();
        }

        private void cmbTampilan_SelectedIndexChanged(object sender, EventArgs e) => LoadChart();

        private void cmbTahun_SelectedIndexChanged(object sender, EventArgs e) => LoadChart();
    }
}
