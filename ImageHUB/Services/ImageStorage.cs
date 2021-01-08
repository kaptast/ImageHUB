using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ImageHUB.Services
{
    public class ImageStorage : IImageStorage
    {
        private readonly IConfiguration configuration;

        public ImageStorage(IConfiguration conf)
        {
            this.configuration = conf;
        }

        public async Task StoreAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new System.Exception("Invalid file.");
            }

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), this.configuration["ImageSavePath"], file.FileName);

            using (var fileStream = new FileStream(pathToSave, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public string StoreBase64(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                return s;
            }
        }
    }
}