using Microsoft.AspNetCore.Mvc;
using PerpustakaanApi.Models;

namespace PerpustakaanApi.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private string _constr;
        private IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        [HttpPost("api/login")]
        public IEnumerable<Login> LoginUser(string email, string password)
        {
            LoginContext context = new LoginContext(_constr);
            List<Login> listLogin = context.Authentifikasi(email, password, _config);
            return listLogin.ToArray();
        }
    }
}