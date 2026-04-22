📚 REST API Manajemen Perpustakaan
Sistem backend berbasis REST API untuk mengelola data perpustakaan, mencakup manajemen pengguna, koleksi buku, dan transaksi peminjaman buku. API dilengkapi dengan autentikasi menggunakan JSON Web Token (JWT).

🗂️ Domain
Sistem Manajemen Perpustakaan - Sistem ini dirancang untuk mengelola tiga entitas utama:
1. Users — data anggota/pengguna perpustakaan
2. Books — data koleksi buku yang tersedia
3. Borrowings — data transaksi peminjaman dan pengembalian buku

Relasi antar entitas: seorang user dapat meminjam banyak buku, dan satu buku dapat dipinjam berkali-kali — dihubungkan melalui tabel borrowings sebagai junction table dengan foreign key ke users dan books.
