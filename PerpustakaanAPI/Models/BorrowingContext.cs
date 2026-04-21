using Npgsql;
using PerpustakaanApi.Helpers;

namespace PerpustakaanApi.Models
{
    public class BorrowingContext
    {
        private string _constr;

        public BorrowingContext(string pConstr)
        {
            _constr = pConstr;
        }

        public List<Borrowing> ListBorrowing()
        {
            List<Borrowing> list = new List<Borrowing>();
            string query = @"SELECT b.id, b.user_id, b.book_id, u.nama as nama_user, 
                            bk.judul as judul_buku, b.tanggal_pinjam, b.tanggal_kembali,
                            b.created_at, b.updated_at
                            FROM borrowings b
                            INNER JOIN users u ON b.user_id = u.id
                            INNER JOIN books bk ON b.book_id = bk.id
                            ORDER BY b.id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Borrowing()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        user_id = int.Parse(reader["user_id"].ToString()),
                        book_id = int.Parse(reader["book_id"].ToString()),
                        nama_user = reader["nama_user"].ToString(),
                        judul_buku = reader["judul_buku"].ToString(),
                        tanggal_pinjam = DateTime.Parse(reader["tanggal_pinjam"].ToString()),
                        tanggal_kembali = reader["tanggal_kembali"] == DBNull.Value ? null : DateTime.Parse(reader["tanggal_kembali"].ToString()),
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

        public Borrowing GetBorrowingById(int id)
        {
            Borrowing borrowing = null;
            string query = @"SELECT b.id, b.user_id, b.book_id, u.nama as nama_user, 
                            bk.judul as judul_buku, b.tanggal_pinjam, b.tanggal_kembali,
                            b.created_at, b.updated_at
                            FROM borrowings b
                            INNER JOIN users u ON b.user_id = u.id
                            INNER JOIN books bk ON b.book_id = bk.id
                            WHERE b.id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    borrowing = new Borrowing()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        user_id = int.Parse(reader["user_id"].ToString()),
                        book_id = int.Parse(reader["book_id"].ToString()),
                        nama_user = reader["nama_user"].ToString(),
                        judul_buku = reader["judul_buku"].ToString(),
                        tanggal_pinjam = DateTime.Parse(reader["tanggal_pinjam"].ToString()),
                        tanggal_kembali = reader["tanggal_kembali"] == DBNull.Value ? null : DateTime.Parse(reader["tanggal_kembali"].ToString()),
                        created_at = DateTime.Parse(reader["created_at"].ToString()),
                        updated_at = DateTime.Parse(reader["updated_at"].ToString()),
                    };
                }
                cmd.Dispose();
            }
            catch { }
            db.closeConnection();
            return borrowing;
        }

        public bool AddBorrowing(Borrowing borrowing)
        {
            string query = "INSERT INTO borrowings (user_id, book_id, tanggal_pinjam) VALUES (@user_id, @book_id, CURRENT_TIMESTAMP)";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@user_id", borrowing.user_id);
                cmd.Parameters.AddWithValue("@book_id", borrowing.book_id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = true;
            }
            catch { }
            db.closeConnection();
            return success;
        }

        public bool ReturnBook(int id)
        {
            string query = "UPDATE borrowings SET tanggal_kembali = CURRENT_TIMESTAMP, updated_at = CURRENT_TIMESTAMP WHERE id = @id";
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

        public bool DeleteBorrowing(int id)
        {
            string query = "DELETE FROM borrowings WHERE id = @id";
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