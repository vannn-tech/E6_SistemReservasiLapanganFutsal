CREATE DATABASE DBFutsalADO;
GO

USE DBFutsalADO;
GO



-- Tabel 1: UserAccount
-- Dipakai oleh: FormLogin, FormRegister, FormReservasi
CREATE TABLE UserAccount (
    UserID       INT           IDENTITY(1,1) PRIMARY KEY,
    Nama         NVARCHAR(100) NOT NULL,
    Username     NVARCHAR(50)  NOT NULL,
    PasswordHash NVARCHAR(100) NOT NULL,
    RoleUser     NVARCHAR(20)  NOT NULL DEFAULT 'User',

    CONSTRAINT UQ_Username UNIQUE (Username),
    CONSTRAINT CK_RoleUser CHECK (RoleUser IN ('Admin', 'User'))
);
GO

-- Tabel 2: Lapangan
-- Dipakai oleh: FormLapangan, FormJadwal, FormReservasi, FormUser
CREATE TABLE Lapangan (
    LapanganID   INT           IDENTITY(1,1) PRIMARY KEY,
    NamaLapangan NVARCHAR(100) NOT NULL,
    Lokasi       NVARCHAR(200) NOT NULL,
    Status       NVARCHAR(30)  NOT NULL DEFAULT 'Tersedia',

    CONSTRAINT CK_StatusLapangan CHECK (Status IN ('Tersedia', 'Tidak Tersedia', 'Dipesan'))
);
GO

-- Tabel 3: Jadwal
-- Dipakai oleh: FormJadwal, FormReservasi, FormUser
CREATE TABLE Jadwal (
    JadwalID   INT          IDENTITY(1,1) PRIMARY KEY,
    LapanganID INT          NOT NULL,
    Tanggal    DATE         NOT NULL,
    Jam        TIME(0)      NOT NULL,
    Status     NVARCHAR(30) NOT NULL DEFAULT 'Tersedia',

    CONSTRAINT FK_Jadwal_Lapangan FOREIGN KEY (LapanganID)
        REFERENCES Lapangan(LapanganID)
        ON UPDATE CASCADE
        ON DELETE CASCADE,

    CONSTRAINT CK_StatusJadwal CHECK (Status IN ('Tersedia', 'Dipesan'))
);
GO

-- Tabel 4: Reservasi
-- Dipakai oleh: FormReservasi, FormUser
CREATE TABLE Reservasi (
    ReservasiID      INT           IDENTITY(1,1) PRIMARY KEY,
    UserID           INT           NOT NULL,
    LapanganID       INT           NOT NULL,
    JadwalID         INT           NOT NULL,
    TanggalReservasi DATETIME      NOT NULL DEFAULT GETDATE(),
    Status           NVARCHAR(30)  NOT NULL DEFAULT 'Aktif',

    CONSTRAINT FK_Reservasi_User     FOREIGN KEY (UserID)
        REFERENCES UserAccount(UserID),
    CONSTRAINT FK_Reservasi_Lapangan FOREIGN KEY (LapanganID)
        REFERENCES Lapangan(LapanganID),
    CONSTRAINT FK_Reservasi_Jadwal   FOREIGN KEY (JadwalID)
        REFERENCES Jadwal(JadwalID),

    CONSTRAINT CK_StatusReservasi CHECK (Status IN ('Aktif', 'Selesai', 'Dibatalkan'))
);
GO


INSERT INTO UserAccount (Nama, Username, PasswordHash, RoleUser) VALUES
    ('Administrator',  'admin', 'admin123', 'Admin'),
    ('Budi Santoso',   'budi',  'budi123',  'User'),
    ('Andi Prasetyo',  'andi',  'andi123',  'User'),
    ('Citra Lestari',  'citra', 'citra123', 'User'),
    ('Doni Firmansyah','doni',  'doni123',  'User');
GO


INSERT INTO Lapangan (NamaLapangan, Lokasi, Status) VALUES
    ('Lapangan A', 'Lantai 1 — Blok Utara',   'Tersedia'),
    ('Lapangan B', 'Lantai 1 — Blok Selatan',  'Tersedia'),
    ('Lapangan C', 'Lantai 2 — Blok Utara',   'Tersedia'),
    ('Lapangan D', 'Lantai 2 — Blok Selatan',  'Tersedia'),
    ('Lapangan E', 'Lantai 3 — VIP',           'Tersedia');
GO



-- Lapangan A (ID = 1)
INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES
    (1, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '08:00', 'Tersedia'),
    (1, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '10:00', 'Tersedia'),
    (1, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '14:00', 'Tersedia'),
    (1, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '16:00', 'Tersedia'),
    (1, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '09:00', 'Tersedia'),
    (1, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '13:00', 'Tersedia'),
    (1, DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '08:00', 'Tersedia');

-- Lapangan B (ID = 2)
INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES
    (2, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '09:00', 'Tersedia'),
    (2, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '11:00', 'Tersedia'),
    (2, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '15:00', 'Tersedia'),
    (2, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '08:00', 'Tersedia'),
    (2, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '10:00', 'Tersedia'),
    (2, DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '14:00', 'Tersedia');

-- Lapangan C (ID = 3)
INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES
    (3, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '08:00', 'Tersedia'),
    (3, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '12:00', 'Tersedia'),
    (3, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '16:00', 'Tersedia'),
    (3, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '09:00', 'Tersedia'),
    (3, DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '10:00', 'Tersedia');

-- Lapangan D (ID = 4)
INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES
    (4, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '10:00', 'Tersedia'),
    (4, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '14:00', 'Tersedia'),
    (4, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '08:00', 'Tersedia'),
    (4, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '16:00', 'Tersedia');

-- Lapangan E (ID = 5)
INSERT INTO Jadwal (LapanganID, Tanggal, Jam, Status) VALUES
    (5, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '09:00', 'Tersedia'),
    (5, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '13:00', 'Tersedia'),
    (5, DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '15:00', 'Tersedia');
GO


-- Reservasi 1: budi (UserID=2) pesan Lapangan A JadwalID=1
INSERT INTO Reservasi (UserID, LapanganID, JadwalID, TanggalReservasi, Status)
VALUES (2, 1, 1, DATEADD(DAY, -1, GETDATE()), 'Aktif');
UPDATE Jadwal SET Status = 'Dipesan' WHERE JadwalID = 1;

-- Reservasi 2: andi (UserID=3) pesan Lapangan B JadwalID=8
INSERT INTO Reservasi (UserID, LapanganID, JadwalID, TanggalReservasi, Status)
VALUES (3, 2, 8, DATEADD(DAY, -1, GETDATE()), 'Aktif');
UPDATE Jadwal SET Status = 'Dipesan' WHERE JadwalID = 8;

-- Reservasi 3: budi (UserID=2) — sudah selesai, Lapangan C
INSERT INTO Reservasi (UserID, LapanganID, JadwalID, TanggalReservasi, Status)
VALUES (2, 3, 14, DATEADD(DAY, -7, GETDATE()), 'Selesai');

-- Reservasi 4: citra (UserID=4) pesan Lapangan D JadwalID=20
INSERT INTO Reservasi (UserID, LapanganID, JadwalID, TanggalReservasi, Status)
VALUES (4, 4, 20, GETDATE(), 'Aktif');
UPDATE Jadwal SET Status = 'Dipesan' WHERE JadwalID = 20;

-- Reservasi 5: doni (UserID=5) — dibatalkan
INSERT INTO Reservasi (UserID, LapanganID, JadwalID, TanggalReservasi, Status)
VALUES (5, 1, 7, DATEADD(DAY, -3, GETDATE()), 'Dibatalkan');
GO


SELECT 'UserAccount'  AS Tabel, COUNT(*) AS Jumlah FROM UserAccount  UNION ALL
SELECT 'Lapangan'     AS Tabel, COUNT(*) AS Jumlah FROM Lapangan      UNION ALL
SELECT 'Jadwal'       AS Tabel, COUNT(*) AS Jumlah FROM Jadwal        UNION ALL
SELECT 'Reservasi'    AS Tabel, COUNT(*) AS Jumlah FROM Reservasi;
GO

-- Preview data lengkap reservasi (sama persis dengan query di FormReservasi.cs)
SELECT
    r.ReservasiID,
    u.Nama          AS Pengguna,
    l.NamaLapangan,
    CAST(j.Tanggal AS VARCHAR(10)) + ' ' + LEFT(CAST(j.Jam AS VARCHAR), 5) AS JadwalInfo,
    CONVERT(VARCHAR(16), r.TanggalReservasi, 120)                           AS TglReservasi,
    r.Status
FROM Reservasi r
JOIN UserAccount u ON r.UserID     = u.UserID
JOIN Lapangan l    ON r.LapanganID = l.LapanganID
JOIN Jadwal j      ON r.JadwalID   = j.JadwalID
ORDER BY r.TanggalReservasi DESC;
GO

-- ══════════════════════════════════════════════════════════════════

--  Akun Login Demo:
--    admin / admin123  →  Role: Admin  →  Masuk ke FormAdmin
--    budi  / budi123   →  Role: User   →  Masuk ke FormUser
--    andi  / andi123   →  Role: User
--    citra / citra123  →  Role: User
--    doni  / doni123   →  Role: User

-- ══════════════════════════════════════════════════════════════════
