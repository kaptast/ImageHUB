using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;
        private readonly IImageStorage imageStorage;

        public ImageService(IImageRepository imageRepository, IImageStorage imageStorage)
        {
            this.imageRepository = imageRepository;
            this.imageStorage = imageStorage;
        }
        public async Task SaveImageAsync(IFormFile file)
        {
            await this.imageStorage.StoreAsync(file);
            this.imageRepository.Save(Path.Combine("img", file.FileName));
        }

        public IEnumerable<string> GetAllImageUrls() => this.imageRepository.GetAll();
    }
}
