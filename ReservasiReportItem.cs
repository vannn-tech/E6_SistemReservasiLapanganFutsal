using System;

namespace ReservasiFutsal02
{
    // ─────────────────────────────────────────────────────────────
    //  ReservasiReportItem — class data sederhana untuk menampung
    //  satu baris hasil sp_LaporanReservasi.
    //
    //  Class inilah yang nanti di-drag ke Field Explorer ketika
    //  mendesain file .rpt di Crystal Reports Designer (lihat
    //  catatan di FormCetakLaporan.cs) — sama persis seperti class
    //  "Data" pada Modul 13 Langkah 8.
    // ─────────────────────────────────────────────────────────────
    public class ReservasiReportItem
    {
        public int      ReservasiID      { get; set; }
        public string   Pengguna         { get; set; }
        public string   NamaLapangan     { get; set; }
        public DateTime Tanggal          { get; set; }
        public string   Jam              { get; set; }
        public DateTime TanggalReservasi { get; set; }
        public string   Status           { get; set; }
    }
}
