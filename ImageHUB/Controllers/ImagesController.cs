using System;
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
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        private DatabaseContext context;

        public ImagesController(IImageService imageService, DatabaseContext context)
        {
            this.imageService = imageService;
            this.context = context;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task Upload(IFormFile file)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string name = User.FindFirstValue(ClaimTypes.Name);

            var owner = context.GetProfileByID(id);

            await this.imageService.SaveImageAsync(this.context, file, owner); 
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Repositories.Post> Get() => this.imageService.GetAllImageUrls(this.context);
    }
}