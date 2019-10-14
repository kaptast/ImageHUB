using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Upload(IFormFile file) => await this.imageService.SaveImageAsync(file);

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get() => this.imageService.GetAllImageUrls();
    }
}