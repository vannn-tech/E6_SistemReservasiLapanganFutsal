using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

/* ════════════════════════════════════════════════════════════════════════
   FormCetakLaporan.cs — versi CRYSTAL REPORTS dari Laporan Reservasi
   (mengikuti Modul Praktikum 13 — Keamanan Pada Fitur Reporting Aplikasi
   Winforms Menggunakan Crystal Reports).

   FILE INI BELUM DIIKUTKAN ke ReservasiFutsal02.csproj, karena butuh:
     1. SAP Crystal Reports runtime + SDK ter-install (lihat Modul 13
        Langkah 2: download "CR for Visual Studio SP40 ...").
     2. Reference ke 4 DLL: CrystalDecisions.CrystalReports.Engine,
        CrystalDecisions.Shared, CrystalDecisions.Windows.Forms,
        CrystalDecisions.ReportSource.
     3. Sebuah file .rpt yang DIDESAIN MANUAL lewat GUI Crystal Report
        Designer di Visual Studio (drag & drop field dari class
        ReservasiReportItem) — ini TIDAK BISA digenerate lewat kode,
        karena .rpt adalah file biner hasil GUI designer, persis seperti
        di Modul 13 Langkah 10.

   CARA MENGAKTIFKAN (singkat — detail lengkap di README_FITUR_TAMBAHAN.md):
     a. Install Crystal Reports runtime & reference 4 DLL di atas.
     b. Pindahkan folder ini (OptionalCrystalReport) ke root project,
        atau biarkan di sini lalu tambahkan kedua file ini ke .csproj:
            <Compile Include="OptionalCrystalReport\FormCetakLaporan.cs" />
            <Compile Include="OptionalCrystalReport\FormCetakLaporan.Designer.cs" />
     c. Klik kanan project → Add → New Item → Crystal Report → beri nama
        "LaporanReservasi.rpt" → pilih "As a Blank Report".
     d. Di Field Explorer: klik kanan Database Fields → Database Expert
        → Project Data → .NET Objects → pilih class ReservasiReportItem
        → OK. (sama seperti Modul 13 Langkah 9-10)
     e. Drag field-field (Pengguna, NamaLapangan, Tanggal, Jam,
        TanggalReservasi, Status) ke Section3 (Details).
     f. Set rptPath di bawah ke nama file .rpt yang baru dibuat.
     g. Di FormLaporanReservasi.cs, ganti isi btnCrystalReport_Click
        menjadi:
            new FormCetakLaporan(lapanganID, status, tahun).Show();
   ════════════════════════════════════════════════════════════════════════ */

namespace ReservasiFutsal02
{
    public partial class FormCetakLaporan : Form
    {
        string connectionString = @"Data Source=10.200.161.237\MSSQLSERVER01;Initial Catalog=DBFutsalADO;User ID=sa;Password=jovan1532006";

        private int    _lapanganID;
        private string _status;
        private int    _tahun;

        public FormCetakLaporan(int lapanganID, string status, int tahun)
        {
            InitializeComponent();
            _lapanganID = lapanganID;
            _status     = status;
            _tahun      = tahun;

            try
            {
                List<ReservasiReportItem> data = AmbilDataLaporan();

            }
            catch (Exception ex)
            {
                AppLogger.SimpanLog("FormCetakLaporan", ex.Message);
                MessageBox.Show("Gagal memuat laporan: " + ex.Message);
            }
        }

        // ── Ambil data sama persis dengan FormLaporanReservasi, lalu
        //    petakan ke List<ReservasiReportItem> untuk dijadikan
        //    DataSource Crystal Report (SetDataSource menerima List<T>).
        private List<ReservasiReportItem> AmbilDataLaporan()
        {
            var list = new List<ReservasiReportItem>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LaporanReservasi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LapanganID", _lapanganID);
                cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(_status) ? (object)DBNull.Value : _status);
                cmd.Parameters.AddWithValue("@Tahun", _tahun);

                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ReservasiReportItem
                        {
                            ReservasiID      = (int)rd["ReservasiID"],
                            Pengguna         = rd["Pengguna"].ToString(),
                            NamaLapangan     = rd["NamaLapangan"].ToString(),
                            Tanggal          = Convert.ToDateTime(rd["Tanggal"]),
                            Jam              = rd["Jam"].ToString(),
                            TanggalReservasi = Convert.ToDateTime(rd["TanggalReservasi"]),
                            Status           = rd["Status"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        private void pnlPlaceholder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPlaceholder_Click(object sender, EventArgs e)
        {

        }
    }
}
