namespace PerpustakaanApi.Models
{
    public class Borrowing
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int book_id { get; set; }
        public string nama_user { get; set; }
        public string judul_buku { get; set; }
        public DateTime tanggal_pinjam { get; set; }
        public DateTime? tanggal_kembali { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}