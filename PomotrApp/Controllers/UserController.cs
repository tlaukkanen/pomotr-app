using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PomotrApp.Data;

namespace PomotrApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public class LoginData {
            public string Username { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public class RegistrationData
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool Consent { get; set; }
            public Guid InvitationID { get; set; }
        }

        private ApplicationDbContext _context;

        public UserController(ApplicationDbContext context) {
            this._context = context;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginData loginData)
        {
            return Ok();
        }

        [HttpPost("register")]
        public ActionResult Register(RegistrationData registrationData)
        {

            return Ok();
        }
        
    }
}