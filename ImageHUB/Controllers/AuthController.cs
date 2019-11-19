using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IProfileService profileService;
        public AuthController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Route("isloggedin")]
        [Authorize]
        public Profile IsLoggedIn()
        {
            string userName = HttpContext.User.Identity.Name;
            string id = Hashes.ComputeSha256Hash(userName);
            //string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = this.profileService.GetProfileByID(id, userName);
            return profile;
            /*var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = this.profileService.GetProfileByID(id, username);

            return user;*/
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