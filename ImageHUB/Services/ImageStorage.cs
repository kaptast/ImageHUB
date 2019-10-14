using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public class ImageStorage : IImageStorage
    {
        private readonly IConfiguration configuration;

        public ImageStorage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task StoreAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new System.Exception("Invalid file.");
            }

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), this.configuration["Image.SavePath"], file.FileName);
            using (var fileStream = new FileStream(pathToSave, FileMode.Create))
            {
               await file.CopyToAsync(fileStream);
            }
        }
    }
}
