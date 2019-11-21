using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IProfileService profileService;

        public AuthController(IProfileService pService)
        {
            profileService = pService;
        }

        [Route("isloggedin")]
        [Authorize]
        public string IsLoggedIn()
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
            await HttpContext.SignOutAsync();
            return this.Ok();
        }
    }
}