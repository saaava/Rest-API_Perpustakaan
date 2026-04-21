using Microsoft.IdentityModel.Tokens;
using Npgsql;
using PerpustakaanApi.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PerpustakaanApi.Models
{
    public class LoginContext
    {
        private string _constr;
        private string _ErrorMsg;

        public LoginContext(string pConstr)
        {
            _constr = pConstr;
        }

        public List<Login> Authentifikasi(string p_email, string p_password, IConfiguration p_config)
        {
            List<Login> list1 = new List<Login>();
            string query = string.Format(@"SELECT id, nama, email FROM users 
                            WHERE email='{0}' and password='{1}'", p_email, p_password);
            SqlDBHelper db = new SqlDBHelper(_constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Login()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        nama = reader["nama"].ToString(),
                        email = reader["email"].ToString(),
                        token = GenerateJwtToken(reader["email"].ToString(), p_config)
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                _ErrorMsg = ex.Message;
            }
            return list1;
        }

        private string GenerateJwtToken(string emailUser, IConfiguration pConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, emailUser),
            };
            var token = new JwtSecurityToken(pConfig["Jwt:Issuer"],
                pConfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}