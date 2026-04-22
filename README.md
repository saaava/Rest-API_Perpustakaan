📚 REST API Manajemen Perpustakaan
Sistem backend berbasis REST API untuk mengelola data perpustakaan, mencakup manajemen pengguna, koleksi buku, dan transaksi peminjaman buku. API dilengkapi dengan autentikasi menggunakan JSON Web Token (JWT).

🗂️ Domain
Sistem Manajemen Perpustakaan - Sistem ini dirancang untuk mengelola tiga entitas utama:
1. Users — data anggota/pengguna perpustakaan
2. Books — data koleksi buku yang tersedia
3. Borrowings — data transaksi peminjaman dan pengembalian buku

Relasi antar entitas: seorang user dapat meminjam banyak buku, dan satu buku dapat dipinjam berkali-kali — dihubungkan melalui tabel borrowings sebagai junction table dengan foreign key ke users dan books.

🛠️ Teknologi yang Digunakan
Bahasa Pemrograman - C#
Framework - ASP.NET Core 7.0
Database - PostgreSQL
Database Client - pgAdmin 4
Driver Database Npgsql
Autentikasi - JSON Web Token (JWT)
Dokumentasi API - Swagger
IDE - Visual Studio 2022

⚙️ Langkah Instalasi dan Cara Menjalankan Project
1. Clone Repository
   git clone https://github.com/USERNAME/NAMA-REPO.git
   cd NAMA-REPO
2. Import Database
3. Konfigurasi Connection String di file appsetting.json
4. Install NuGet Package
5. Jalankan Project dengan tekan tombol Run

🗄️ Cara Import Database
1. Klik kanan database perpustakaan, pilih query tool
2. Klik ikon open file (folder) pilih file perpustakaan.sql
3. Klik tombol Execute/Run
