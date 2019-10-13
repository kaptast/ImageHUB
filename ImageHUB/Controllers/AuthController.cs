using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("isloggedin")]
        [Authorize]
        public IActionResult IsLoggedIn()
        {
            return Ok();
        }

        [Route("signin")]
        public IActionResult SingInWithFacebook()
        {
            var redirectUrl = Url.Action(nameof(AuthController.SignInCallback), "Auth", new { returnUrl = "/" });
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, FacebookDefaults.AuthenticationScheme);
        }

        [Route("callback")]
        [HttpGet]
        public async Task<IActionResult> SignInCallback(string returnUrl)
        {
            var authencticateResult = await HttpContext.AuthenticateAsync();
            await HttpContext.SignInAsync(authencticateResult.Ticket.Principal);
            return this.LocalRedirect(returnUrl);
        }


        // TO DO : LOGOUT


    }

}