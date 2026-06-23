using System;
using System.Data;
using System.Data.SqlClient;

namespace ReservasiFutsal02
{
    internal static class AppLogger
    {
        // Samakan dengan connection string yang dipakai Form lain di project ini.
        private const string ConnectionString =
            @"Data Source=10.200.161.237\MSSQLSERVER01;Initial Catalog=DBFutsalADO;User ID=sa;Password=jovan1532006";

        public static void SimpanLog(string sumber, string pesan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = @"INSERT INTO LogError (Waktu, Sumber, PesanError)
                                      VALUES (GETDATE(), @Sumber, @Pesan)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sumber", string.IsNullOrEmpty(sumber) ? "Unknown" : sumber);
                        cmd.Parameters.AddWithValue("@Pesan", pesan ?? "");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Sengaja diabaikan — lihat penjelasan di atas.
            }
        }

        public static void SimpanLog(string pesan) => SimpanLog(null, pesan);

        public static void SimpanAktivitas(string aktivitas)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = @"INSERT INTO LogAktivitas (Aksi, Tabel, Detail, WaktuLog)
                              VALUES (@Aksi, @Tabel, @Detail, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Aksi", "IMPORT");
                        cmd.Parameters.AddWithValue("@Tabel", "Jadwal");
                        cmd.Parameters.AddWithValue("@Detail", aktivitas ?? "");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Sengaja diabaikan, sama seperti SimpanLog().
            }
        }
    }
}
