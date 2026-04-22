# 👤 Identitas

| Nama | Sava Tiara Nathania           |
| ------ | ------------- |
| NIM   | 242410102009 |
| Kelas   | PAA B    | 

# 📚 REST API Manajemen Perpustakaan
Sistem backend berbasis REST API untuk mengelola data perpustakaan, mencakup manajemen pengguna, koleksi buku, dan transaksi peminjaman buku. API dilengkapi dengan autentikasi menggunakan JSON Web Token (JWT).

# 🗂️ Domain
Sistem Manajemen Perpustakaan - Sistem ini dirancang untuk mengelola tiga entitas utama:
1. Users — data anggota/pengguna perpustakaan
2. Books — data koleksi buku yang tersedia
3. Borrowings — data transaksi peminjaman dan pengembalian buku

Relasi antar entitas: seorang user dapat meminjam banyak buku, dan satu buku dapat dipinjam berkali-kali — dihubungkan melalui tabel borrowings sebagai junction table dengan foreign key ke users dan books.

# 🛠️ Teknologi yang Digunakan
* Bahasa Pemrograman - C#
* Framework - ASP.NET Core 7.0
* Database - PostgreSQL
* Database Client - pgAdmin 4
* Driver Database Npgsql
* Autentikasi - JSON Web Token (JWT)
* Dokumentasi API - Swagger
* IDE - Visual Studio 2022

# ⚙️ Langkah Instalasi dan Cara Menjalankan Project
1. Clone Repository
   * git clone https://github.com/USERNAME/NAMA-REPO.git
   * cd NAMA-REPO
2. Import Database
3. Konfigurasi Connection String di file appsetting.json
4. Install NuGet Package
5. Jalankan Project dengan tekan tombol Run

# 🗄️ Cara Import Database
1. Klik kanan database perpustakaan, pilih query tool
2. Klik ikon open file (folder) pilih file perpustakaan.sql
3. Klik tombol Execute/Run

# 📡 Daftar Endpoint Lengkap

| Method | URL             | Keterangan                           | Auth  |
| ------ | --------------- | ------------------------------------ | ----- |
| GET    | /api/users      | Mengambil semua data user            | ✅ JWT |
| GET    | /api/users/{id} | Mengambil detail user berdasarkan ID | ✅ JWT |
| PUT    | /api/users/{id} | Mengupdate data user berdasarkan ID  | ✅ JWT |
| DELETE | /api/users/{id} | Menghapus user berdasarkan ID        | ✅ JWT |

| Method | URL             | Keterangan                           | Auth          |
| ------ | --------------- | ------------------------------------ | ------------- |
| GET    | /api/books      | Mengambil semua data buku            | ❌ Tidak perlu |
| GET    | /api/books/{id} | Mengambil detail buku berdasarkan ID | ❌ Tidak perlu |
| POST   | /api/books      | Menambahkan buku baru                | ✅ JWT         |
| PUT    | /api/books/{id} | Mengupdate data buku berdasarkan ID  | ✅ JWT         |
| DELETE | /api/books/{id} | Menghapus buku berdasarkan ID        | ✅ JWT         |

| Method | URL                         | Keterangan                                   | Auth  |
| ------ | --------------------------- | -------------------------------------------- | ----- |
| GET    | /api/borrowings             | Mengambil semua data peminjaman              | ✅ JWT |
| GET    | /api/borrowings/{id}        | Mengambil detail peminjaman berdasarkan ID   | ✅ JWT |
| POST   | /api/borrowings             | Mencatat peminjaman buku baru                | ✅ JWT |
| PUT    | /api/borrowings/{id}/return | Mengembalikan buku (mengisi tanggal_kembali) | ✅ JWT |
| DELETE | /api/borrowings/{id}        | Menghapus riwayat peminjaman                 | ✅ JWT |

| Method | URL           | Keterangan                      | Auth          |
| ------ | ------------- | ------------------------------- | ------------- |
| POST   | /api/register | Registrasi user baru            | ❌ Tidak perlu |
| POST   | /api/login    | Login dan mendapatkan JWT token | ❌ Tidak perlu |

# 🎥 Video Presentasi
* 📺 Link video presentasi: 
https://youtu.be/QCdVfChQIzQ
