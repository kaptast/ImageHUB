using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly ILogger<Startup> logger;

        public AuthController(IProfileService pService, ILogger<Startup> logger)
        {
            this.profileService = pService;
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

            var profile = this.profileService.GetProfileByID(userId, userName);

            if (profile == null)
            {
                this.profileService.AddProfile(userId, userName);
            }

            return userName;

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
            logger.LogInformation("User logout");
            /*await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Ok();*/
            await SignOut("/login");
            //HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Ok();
        }

        public async Task SignOut(string redirectUri)
        {
            // inject the HttpContextAccessor to get "context"
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var prop = new AuthenticationProperties()
            {
                RedirectUri = redirectUri
            };
            // after signout this will redirect to your provided target
            await HttpContext.SignOutAsync("oidc", prop);
        }
    }
}