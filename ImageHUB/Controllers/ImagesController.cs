﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IProfileService profileService;

        public ImagesController(IImageService imageService, IProfileService profileService)
        {
            this.imageService = imageService;
            this.profileService = profileService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string name = HttpContext.User.Identity.Name;
            var userId = Hashes.ComputeSha256Hash(name);

            var owner = this.profileService.GetProfileByID(userId, name);

            await this.imageService.SaveImageAsync(file, owner);

            return Redirect("/");
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = this.imageService.GetAllImageUrls(id);
            return posts;
        }
    }
}