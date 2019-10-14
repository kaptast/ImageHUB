using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task Upload(IFormFile file)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string name = User.FindFirstValue(ClaimTypes.Name);

            await this.imageService.SaveImageAsync(file, id, name);
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Repositories.Post> Get() => this.imageService.GetAllImageUrls();
    }
}