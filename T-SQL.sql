USE DBFutsalADO;
GO

/* ════════════════════════════════════════════════════════════════════════
   BAGIAN 1 — TABEL LOGGING  (Modul 11, Praktikum 1)
   ════════════════════════════════════════════════════════════════════════ */

-- 1a. LogError — menyimpan pesan SqlException / Exception dari aplikasi
IF OBJECT_ID('dbo.LogError', 'U') IS NULL
BEGIN
    CREATE TABLE LogError
    (
        LogID      INT IDENTITY(1,1) PRIMARY KEY,
        Waktu      DATETIME      NOT NULL DEFAULT GETDATE(),
        Sumber     NVARCHAR(50)  NULL,        -- nama Form/aksi asal error
        PesanError NVARCHAR(MAX) NOT NULL
    );
END
GO

-- 1b. LogAktivitas — dicatat otomatis oleh trigger (INSERT Jadwal, UPDATE status Reservasi)
IF OBJECT_ID('dbo.LogAktivitas', 'U') IS NULL
BEGIN
    CREATE TABLE LogAktivitas
    (
        LogID     INT IDENTITY(1,1) PRIMARY KEY,
        Aktivitas NVARCHAR(300) NOT NULL,
        Waktu     DATETIME      NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 1c. LogKeamanan — dicatat otomatis oleh trigger anti-update massal
IF OBJECT_ID('dbo.LogKeamanan', 'U') IS NULL
BEGIN
    CREATE TABLE LogKeamanan
    (
        LogID      INT IDENTITY(1,1) PRIMARY KEY,
        Aktivitas  NVARCHAR(300) NOT NULL,
        JumlahData INT           NULL,
        Waktu      DATETIME      NOT NULL DEFAULT GETDATE()
    );
END
GO


/* ════════════════════════════════════════════════════════════════════════
   BAGIAN 2 — TRIGGER MONITORING & KEAMANAN  (Modul 11, Praktikum 6-8)
   ════════════════════════════════════════════════════════════════════════ */

-- 2a. trg_InsertJadwal
--     Mencatat setiap penambahan slot jadwal baru — baik ditambah satu-satu
--     lewat Form Jadwal (btnSimpan), MAUPUN lewat fitur Import Excel yang
--     baru (karena Import Excel memanggil sp_InsertJadwal yang sama persis
--     per baris, trigger ini otomatis ikut tercatat tanpa kode tambahan).
-- Ganti trg_InsertJadwal agar pakai struktur LogAktivitas yang sudah ada
IF OBJECT_ID('dbo.trg_InsertJadwal', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_InsertJadwal;
GO
CREATE TRIGGER trg_InsertJadwal
ON Jadwal
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LogAktivitas (Aksi, Tabel, Detail, WaktuLog)
    SELECT
        'INSERT',
        'Jadwal',
        'Tambah Jadwal -> LapanganID=' + CAST(i.LapanganID AS VARCHAR(10))
            + ' | ' + CONVERT(VARCHAR(10), i.Tanggal, 103)
            + ' ' + CONVERT(VARCHAR(5), i.Jam),
        GETDATE()
    FROM inserted i;
END;
GO

-- Ganti trg_UpdateStatusReservasi agar pakai struktur LogAktivitas yang sudah ada
IF OBJECT_ID('dbo.trg_UpdateStatusReservasi', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_UpdateStatusReservasi;
GO
CREATE TRIGGER trg_UpdateStatusReservasi
ON Reservasi
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(Status)
    BEGIN
        INSERT INTO LogAktivitas (Aksi, Tabel, Detail, WaktuLog)
        SELECT
            'UPDATE',
            'Reservasi',
            'Status Reservasi #' + CAST(i.ReservasiID AS VARCHAR(10)) + ' -> ' + i.Status,
            GETDATE()
        FROM inserted i;
    END
END;
GO

SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'LogError';
SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'LogKeamanan';

-- 2c. trg_PreventMassUpdateLapangan  ("Trigger Pencegahan Update Data Besar-Besaran")
--     Operasi normal (btnUbah / sp_UpdateLapangan) HANYA PERNAH mengubah
--     1 baris (berdasarkan LapanganID), jadi ambang batas > 2 baris aman
--     dipakai sebagai tanda "tidak normal".
--
--     Trigger ini SENGAJA dipasang untuk menahan tombol "🗿 Inject" yang
--     SUDAH ADA di FormLapangan (btnTestInjection_Click) — yaitu query
--     rentan SQL Injection:
--         UPDATE Lapangan SET Lokasi='DIHACK' WHERE NamaLapangan='<input>'
--     Jika diisi payload semacam  x' OR '1'='1  maka WHERE menjadi selalu
--     TRUE dan akan meng-update SEMUA baris Lapangan sekaligus.
--     Trigger ini mendeteksi itu, lalu ROLLBACK + RAISERROR + catat ke
--     LogKeamanan, sehingga payload tadi tidak benar-benar mengubah data.
IF OBJECT_ID('dbo.trg_PreventMassUpdateLapangan', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_PreventMassUpdateLapangan;
GO
CREATE TRIGGER trg_PreventMassUpdateLapangan
ON Lapangan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @jumlah INT;
    SELECT @jumlah = COUNT(*) FROM inserted;

    IF @jumlah > 2
    BEGIN
        INSERT INTO LogKeamanan (Aktivitas, JumlahData, Waktu)
        VALUES ('WARNING: Percobaan UPDATE massal terdeteksi pada tabel Lapangan (kemungkinan SQL Injection)',
                @jumlah, GETDATE());

        ROLLBACK TRANSACTION;
        RAISERROR('Update dibatalkan! Terdeteksi %d baris ter-update sekaligus (indikasi SQL Injection / update massal).', 16, 1, @jumlah);
    END
END;
GO


/* ════════════════════════════════════════════════════════════════════════
   BAGIAN 3 — STORED PROCEDURE LAPORAN  (Modul 13)
   Dipakai oleh: FormLaporanReservasi.cs
   ════════════════════════════════════════════════════════════════════════ */

IF OBJECT_ID('dbo.sp_LaporanReservasi', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_LaporanReservasi;
GO
CREATE PROCEDURE sp_LaporanReservasi
    @LapanganID INT          = 0,     -- 0      = semua lapangan
    @Status     NVARCHAR(30) = NULL,  -- NULL/''= semua status
    @Tahun      INT          = 0      -- 0      = semua tahun
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        r.ReservasiID,
        u.Nama                                AS Pengguna,
        l.NamaLapangan,
        j.Tanggal,
        LEFT(CAST(j.Jam AS VARCHAR(8)), 5)     AS Jam,
        r.TanggalReservasi,
        r.Status
    FROM Reservasi r
    JOIN UserAccount u ON r.UserID     = u.UserID
    JOIN Lapangan    l ON r.LapanganID = l.LapanganID
    JOIN Jadwal      j ON r.JadwalID   = j.JadwalID
    WHERE (@LapanganID = 0 OR r.LapanganID = @LapanganID)
      AND (@Status IS NULL OR @Status = '' OR r.Status = @Status)
      AND (@Tahun = 0 OR YEAR(r.TanggalReservasi) = @Tahun)
    ORDER BY r.TanggalReservasi DESC;
END;
GO


/* ════════════════════════════════════════════════════════════════════════
   BAGIAN 4 — STORED PROCEDURE DASHBOARD / GRAFIK  (Modul 14)
   Dipakai oleh: FormGrafik.cs
   ════════════════════════════════════════════════════════════════════════ */

-- 4a. Jumlah reservasi per lapangan -> grafik kolom
IF OBJECT_ID('dbo.sp_DashboardPerLapangan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DashboardPerLapangan;
GO
CREATE PROCEDURE sp_DashboardPerLapangan
    @Tahun INT = 0          -- 0 = semua tahun
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        l.NamaLapangan,
        COUNT(r.ReservasiID) AS JumlahReservasi
    FROM Lapangan l
    LEFT JOIN Reservasi r
        ON r.LapanganID = l.LapanganID
       AND (@Tahun = 0 OR YEAR(r.TanggalReservasi) = @Tahun)
    GROUP BY l.NamaLapangan
    ORDER BY l.NamaLapangan;
END;
GO

-- 4b. Jumlah reservasi per status -> grafik pie
IF OBJECT_ID('dbo.sp_DashboardPerStatus', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DashboardPerStatus;
GO
CREATE PROCEDURE sp_DashboardPerStatus
    @Tahun INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        r.Status,
        COUNT(*) AS Jumlah
    FROM Reservasi r
    WHERE (@Tahun = 0 OR YEAR(r.TanggalReservasi) = @Tahun)
    GROUP BY r.Status;
END;
GO

-- 4c. Tren reservasi per bulan (untuk tahun tertentu) -> grafik kolom/garis
IF OBJECT_ID('dbo.sp_DashboardPerBulan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DashboardPerBulan;
GO
CREATE PROCEDURE sp_DashboardPerBulan
    @Tahun INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        MONTH(r.TanggalReservasi) AS Bulan,
        COUNT(*)                 AS Jumlah
    FROM Reservasi r
    WHERE YEAR(r.TanggalReservasi) = @Tahun
    GROUP BY MONTH(r.TanggalReservasi)
    ORDER BY Bulan;
END;
GO


/* ════════════════════════════════════════════════════════════════════════
   BAGIAN 5 — VERIFIKASI CEPAT
   Jalankan blok ini untuk memastikan semua objek baru berhasil dibuat.
   ════════════════════════════════════════════════════════════════════════ */
SELECT name AS NamaTrigger, OBJECT_NAME(parent_id) AS PadaTabel
FROM sys.triggers
WHERE name IN ('trg_InsertJadwal','trg_UpdateStatusReservasi','trg_PreventMassUpdateLapangan');

SELECT name AS NamaProcedure
FROM sys.procedures
WHERE name IN ('sp_LaporanReservasi','sp_DashboardPerLapangan','sp_DashboardPerStatus','sp_DashboardPerBulan');

SELECT 'LogError' AS Tabel, COUNT(*) AS Jumlah FROM LogError      UNION ALL
SELECT 'LogAktivitas',      COUNT(*)            FROM LogAktivitas UNION ALL
SELECT 'LogKeamanan',       COUNT(*)            FROM LogKeamanan;
GO