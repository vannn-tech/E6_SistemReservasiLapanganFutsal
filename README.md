# 🏟️ Sistem Reservasi Futsal02

Aplikasi manajemen reservasi lapangan futsal berbasis desktop yang dibangun menggunakan **C# .NET WinForms** dan **SQL Server (ADO.NET)**. Proyek ini dirancang untuk mempermudah administrator dalam mengelola data lapangan, jadwal, dan transaksi pemesanan secara efisien.

## 🚀 Fitur Utama
* **Autentikasi Multi-Role:** Login khusus untuk Admin dan User.
* **Manajemen Lapangan (CRUD):** Tambah, Lihat, Ubah, dan Hapus data lapangan futsal.
* **Manajemen Jadwal:** Pengaturan slot waktu operasional lapangan.
* **Pencarian Cepat:** Filter data berdasarkan nama lapangan secara real-time.
* **Antarmuka Modern:** Desain Dark Mode yang konsisten menggunakan `UITheme`.

---

## 📸 Dokumentasi Sistem

### 1. Form Koneksi (Form Login)
Aplikasi terhubung ke SQL Server menggunakan ADO.NET dengan string koneksi:
string connectionString = @"Data Source=LAPTOP-5R80O1Q5\MSSQLSERVER01;Initial Catalog=DBFutsalADO;Integrated Security=True";

Sistem terhubung ke database `DBFutsalADO`. Pengguna harus masuk menggunakan akun yang terdaftar untuk mengakses panel kontrol.
- **Bukti:** Berhasil masuk ke Dashboard Admin setelah validasi kredensial.

- <img width="416" height="589" alt="image" src="https://github.com/user-attachments/assets/29e62d3e-1ba2-4e66-b49b-1bc4f29525f6" />
- <img width="1020" height="732" alt="image" src="https://github.com/user-attachments/assets/796aa4f7-7a21-43be-b3ba-ae9f6b9f676f" />

### 2. Form Input Data (Insert)
Proses penambahan data baru ke dalam sistem.
- **Aksi:** Mengisi detail pada panel kiri dan menekan tombol **Simpan**.

<img width="1014" height="735" alt="image" src="https://github.com/user-attachments/assets/0786f8ca-3c2c-4829-8f5b-74b5846a92d6" />


### 3. Form Tampilan Data (Read)
Menampilkan seluruh data dari SQL Server ke dalam `DataGridView`.
- **Fitur:** Sinkronisasi otomatis setiap kali ada perubahan data.

<img width="1025" height="727" alt="image" src="https://github.com/user-attachments/assets/2fd17e30-c201-472d-8aac-737d6a66605f" />

### 4. Insert, Update, Delete, & Search
- **Insert:** <img width="1018" height="731" alt="image" src="https://github.com/user-attachments/assets/ebe8f750-9a56-49df-80c0-31ba467eb268" />

- **Update:** <img width="1019" height="733" alt="image" src="https://github.com/user-attachments/assets/07ec39c8-42d6-4738-ab08-96b9f5e19f1e" />

- **Delete:** <img width="1026" height="740" alt="image" src="https://github.com/user-attachments/assets/41851241-5d2c-4b4f-92d1-44007579dfc5" />

- **Search:** <img width="1019" height="734" alt="image" src="https://github.com/user-attachments/assets/b2b456d8-de18-4ffe-8aea-e55cc6d91db9" />

---

🛡️ Skenario Pengujian Keamanan: SQL Injection
Bagian ini mendokumentasikan pemenuhan Kriteria 3 UCP 2, yaitu demonstrasi celah keamanan SQL Injection dan solusinya.

1. Identifikasi Celah (Vulnerability)
Celah ini secara teoritis terdapat pada fitur Login. Jika sistem menggunakan penggabungan string (concatenation) secara langsung, penyerang dapat memanipulasi logika query SQL.

Lokasi: FormLogin.cs pada event btnLogin_Click.

Contoh Kode Rentan (Vulnerable Code):

C#
string query = "SELECT * FROM UserAccount WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "'";
2. Skenario Serangan (The Attack)
Penyerang ingin masuk ke sistem tanpa memiliki akun atau password yang valid.

Input Username: ' OR 1=1 --

Input Password: (Dikosongkan atau diisi acak)

Logika Manipulasi:
Ketika input tersebut masuk ke kode rentan, query yang dikirim ke SQL Server menjadi:

SQL
SELECT * FROM UserAccount WHERE Username = '' OR 1=1 --' AND Password = ''
Analisis Hasil:

OR 1=1 selalu bernilai TRUE.

Tanda -- adalah komentar di SQL, sehingga pengecekan password di belakangnya diabaikan.

Sistem akan memberikan akses (Login Berhasil) karena query tersebut mengembalikan data user pertama di database.

3. Solusi Pencegahan (Mitigasi)
Dalam proyek UCP 2 ini, pencegahan dilakukan dengan menerapkan Kriteria 1, yaitu menggunakan Stored Procedure dan Parameterized Query.

Kode yang Diterapkan (Secure Code):

C#
SqlCommand cmd = new SqlCommand("sp_Login", conn);
cmd.CommandType = CommandType.StoredProcedure;

cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);
