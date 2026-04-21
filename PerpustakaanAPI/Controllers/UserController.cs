using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerpustakaanApi.Models;

namespace PerpustakaanApi.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private string _constr;

        public UserController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        // GET semua user - butuh JWT
        [HttpGet("api/users"), Authorize]
        public ActionResult<object> GetAllUsers()
        {
            UserContext context = new UserContext(_constr);
            List<User> list = context.ListUser();
            return Ok(new { status = "success", data = list, meta = new { total = list.Count } });
        }

        // GET user by id - butuh JWT
        [HttpGet("api/users/{id}"), Authorize]
        public ActionResult<object> GetUserById(int id)
        {
            UserContext context = new UserContext(_constr);
            User user = context.GetUserById(id);
            if (user == null)
                return NotFound(new { status = "error", message = "User tidak ditemukan" });
            return Ok(new { status = "success", data = user });
        }

        // POST register user - bebas akses
        [HttpPost("api/register")]
        public ActionResult<object> RegisterUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.nama) || string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.password))
                return BadRequest(new { status = "error", message = "Nama, email, dan password wajib diisi" });

            UserContext context = new UserContext(_constr);
            bool success = context.RegisterUser(user);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Gagal registrasi, email mungkin sudah terdaftar" });
            return StatusCode(201, new { status = "success", message = "Registrasi berhasil" });
        }

        // PUT update user - butuh JWT
        [HttpPut("api/users/{id}"), Authorize]
        public ActionResult<object> UpdateUser(int id, [FromBody] User user)
        {
            UserContext context = new UserContext(_constr);
            bool success = context.UpdateUser(id, user);
            if (!success)
                return NotFound(new { status = "error", message = "User tidak ditemukan" });
            return Ok(new { status = "success", message = "User berhasil diupdate" });
        }

        // DELETE user - butuh JWT
        [HttpDelete("api/users/{id}"), Authorize]
        public ActionResult<object> DeleteUser(int id)
        {
            UserContext context = new UserContext(_constr);
            bool success = context.DeleteUser(id);
            if (!success)
                return NotFound(new { status = "error", message = "User tidak ditemukan" });
            return Ok(new { status = "success", message = "User berhasil dihapus" });
        }
    }
}