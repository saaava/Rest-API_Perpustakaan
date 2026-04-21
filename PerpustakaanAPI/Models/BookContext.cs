using Npgsql;
using PerpustakaanApi.Helpers;

namespace PerpustakaanApi.Models
{
    public class BookContext
    {
        private string _constr;

        public BookContext(string pConstr)
        {
            _constr = pConstr;
        }

        public List<Book> ListBook()
        {
            List<Book> list = new List<Book>();
            string query = "SELECT id, judul, penulis, stok, created_at, updated_at FROM books ORDER BY id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Book()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        judul = reader["judul"].ToString(),
                        penulis = reader["penulis"].ToString(),
                        stok = int.Parse(reader["stok"].ToString()),
                        created_at = DateTime.Parse(reader["created_at"].ToString()),
                        updated_at = DateTime.Parse(reader["updated_at"].ToString()),
                    });
                }
                cmd.Dispose();
            }
            catch { }
            db.closeConnection();
            return list;
        }

        public Book GetBookById(int id)
        {
            Book book = null;
            string query = "SELECT id, judul, penulis, stok, created_at, updated_at FROM books WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    book = new Book()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        judul = reader["judul"].ToString(),
                        penulis = reader["penulis"].ToString(),
                        stok = int.Parse(reader["stok"].ToString()),
                        created_at = DateTime.Parse(reader["created_at"].ToString()),
                        updated_at = DateTime.Parse(reader["updated_at"].ToString()),
                    };
                }
                cmd.Dispose();
            }
            catch { }
            db.closeConnection();
            return book;
        }

        public bool AddBook(Book book)
        {
            string query = "INSERT INTO books (judul, penulis, stok) VALUES (@judul, @penulis, @stok)";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@judul", book.judul);
                cmd.Parameters.AddWithValue("@penulis", book.penulis);
                cmd.Parameters.AddWithValue("@stok", book.stok);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = true;
            }
            catch { }
            db.closeConnection();
            return success;
        }

        public bool UpdateBook(int id, Book book)
        {
            string query = "UPDATE books SET judul = @judul, penulis = @penulis, stok = @stok, updated_at = CURRENT_TIMESTAMP WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@judul", book.judul);
                cmd.Parameters.AddWithValue("@penulis", book.penulis);
                cmd.Parameters.AddWithValue("@stok", book.stok);
                cmd.Parameters.AddWithValue("@id", id);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = rows > 0;
            }
            catch { }
            db.closeConnection();
            return success;
        }

        public bool DeleteBook(int id)
        {
            string query = "DELETE FROM books WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = rows > 0;
            }
            catch { }
            db.closeConnection();
            return success;
        }
    }
}