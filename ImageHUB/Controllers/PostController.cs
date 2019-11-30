using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Entities;
using ImageHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IPostService postService;

        public PostController(IProfileService profileService, IPostService postService)
        {
            this.profileService = profileService;
            this.postService = postService;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.profileService.GetProfileByID(userId, userName);

            List<Post> posts = this.postService.GetPostsByUser(userId).ToList();

            foreach (var friend in user.Friends)
            {
                posts.AddRange(this.postService.GetPostsByUser(friend.UserID));
            }

            return posts.OrderByDescending(p => p.ID);
        }

        [HttpPost]
        [Route("upload")]
        public async Task Upload(IFormFile file)
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var owner = this.profileService.GetProfileByID(userId, userName);

            await this.postService.SavePostAsync(file, owner);
        }
    }
}