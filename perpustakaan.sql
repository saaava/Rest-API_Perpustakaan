CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    judul VARCHAR(200) NOT NULL,
    penulis VARCHAR(100) NOT NULL,
    stok INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE borrowings (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(id),
    book_id INT NOT NULL REFERENCES books(id),
    tanggal_pinjam TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    tanggal_kembali TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Index untuk filter
CREATE INDEX idx_borrowings_user_id ON borrowings(user_id);
CREATE INDEX idx_borrowings_book_id ON borrowings(book_id);

INSERT INTO users (nama, email, password) VALUES
('Admin Perpustakaan', 'admin@library.com', 'password123'),
('Budi Santoso', 'budi@gmail.com', 'password123'),
('Siti Rahayu', 'siti@gmail.com', 'password123'),
('Ahmad Fauzan', 'ahmad@gmail.com', 'password123'),
('Dewi Lestari', 'dewi@gmail.com', 'password123');

INSERT INTO books (judul, penulis, stok) VALUES
('Laskar Pelangi', 'Andrea Hirata', 5),
('Bumi Manusia', 'Pramoedya Ananta Toer', 3),
('Negeri 5 Menara', 'Ahmad Fuadi', 4),
('Ayat-Ayat Cinta', 'Habiburrahman El Shirazy', 2),
('Dilan 1990', 'Pidi Baiq', 6);

INSERT INTO borrowings (user_id, book_id, tanggal_pinjam) VALUES
(2, 1, CURRENT_TIMESTAMP),
(3, 2, CURRENT_TIMESTAMP),
(4, 3, CURRENT_TIMESTAMP),
(5, 1, CURRENT_TIMESTAMP),
(2, 4, CURRENT_TIMESTAMP);

select * from users
select * from books