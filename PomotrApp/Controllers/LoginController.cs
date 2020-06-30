using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PomotrApp.Models;

namespace PomotrApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public class LoginViewModel
        {
            [Required]
            [DataType(DataType.EmailAddress, ErrorMessage = "Username must be an email address")]
            [StringLength(256, ErrorMessage = "Email address length can't be more than 256.")]
            [Description("Username as email address")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(512, ErrorMessage = "Password length can't be more than 512.")]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            [Description("Application will remember the session from the same device")]
            public bool RememberMe { get; set; }
            [Description("ReturnUrl for OpenID Connect Authorization flow")]
            public string ReturnUrl { get; set; }
        }        

        private ILogger<LoginController> _logger { get; set; }
        private SignInManager<FamilyMember> _signInManager { get; set; }

        public LoginController(
            ILogger<LoginController> logger,
            SignInManager<FamilyMember> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, model.RememberMe, false);

            if(result.Succeeded)
            {
                _logger.LogInformation("Login succeeded for " + model.Username);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return Redirect("/authentication/callback");
                }
            }

            _logger.LogInformation("Authentication failed for user " + model.Username + " " + result.ToString());
            return Unauthorized();
        }        
    }
}