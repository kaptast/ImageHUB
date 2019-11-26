using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace imagehubsample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<Startup> logger;
        
        public AuthController(ILogger<Startup> logger)
        {
            this.logger = logger;
        }

        [Route("isloggedin")]
        [Authorize]
        public string IsLoggedIn()
        {
            string userName = HttpContext.User.Identity.Name;
            logger.LogInformation("----------------------------------------------------------------------------------------------\nIsLoggedIn UserName: {0}", userName);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            logger.LogInformation("IsLoggedIn userID: {0}", userId);

            /*var profile = this.profileService.GetProfileByID(userId, userName);
            if (profile == null)
            {
                this.profileService.AddProfile(userId, userName);
            }*/

            return userName + " " + userId;
        }

        [Route("signin")]
        public IActionResult SignInWithFacebook()
        {
            var redirectUrl = Url.Action(nameof(AuthController.SignInCallback), "Auth", new { returnUrl = "/" });
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, FacebookDefaults.AuthenticationScheme);
        }

        [Route("callback")]
        [HttpGet]
        public async Task<IActionResult> SignInCallback(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync();
            await HttpContext.SignInAsync(authenticateResult.Ticket.Principal);
            return this.LocalRedirect(returnUrl);
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return this.Ok();
        }
    }
}