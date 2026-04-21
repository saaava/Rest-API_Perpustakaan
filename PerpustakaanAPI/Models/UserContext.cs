using Npgsql;
using PerpustakaanApi.Helpers;

namespace PerpustakaanApi.Models
{
    public class UserContext
    {
        private string _constr;

        public UserContext(string pConstr)
        {
            _constr = pConstr;
        }

        public List<User> ListUser()
        {
            List<User> list = new List<User>();
            string query = "SELECT id, nama, email, created_at, updated_at FROM users ORDER BY id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        nama = reader["nama"].ToString(),
                        email = reader["email"].ToString(),
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

        public User GetUserById(int id)
        {
            User user = null;
            string query = "SELECT id, nama, email, created_at, updated_at FROM users WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        nama = reader["nama"].ToString(),
                        email = reader["email"].ToString(),
                        created_at = DateTime.Parse(reader["created_at"].ToString()),
                        updated_at = DateTime.Parse(reader["updated_at"].ToString()),
                    };
                }
                cmd.Dispose();
            }
            catch { }
            db.closeConnection();
            return user;
        }

        public bool RegisterUser(User user)
        {
            string query = "INSERT INTO users (nama, email, password) VALUES (@nama, @email, @password)";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", user.nama);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = true;
            }
            catch { }
            db.closeConnection();
            return success;
        }

        public bool UpdateUser(int id, User user)
        {
            string query = "UPDATE users SET nama = @nama, email = @email, updated_at = CURRENT_TIMESTAMP WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper(_constr);
            bool success = false;
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", user.nama);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@id", id);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                success = rows > 0;
            }
            catch { }
            db.closeConnection();
            return success;
        }

        public bool DeleteUser(int id)
        {
            string query = "DELETE FROM users WHERE id = @id";
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