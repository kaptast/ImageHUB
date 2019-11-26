using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace imagehubsample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [Route("isloggedin")]
        [Authorize]
        public string IsLoggedIn()
        {
            return HttpContext.User.Identity.Name + " " +  User.FindFirstValue(ClaimTypes.NameIdentifier);
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