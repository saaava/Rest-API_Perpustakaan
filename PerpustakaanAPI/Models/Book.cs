namespace PerpustakaanApi.Models
{
    public class Book
    {
        public int id { get; set; }
        public string judul { get; set; }
        public string penulis { get; set; }
        public int stok { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}