using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IProfileService profileService;
        public AuthController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Route("isloggedin")]
        [Authorize]
        public string IsLoggedIn()
        {
            /*var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = this.profileService.GetProfileByID(id, username);

            return user;*/
            return HttpContext.User.Identity.Name;
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