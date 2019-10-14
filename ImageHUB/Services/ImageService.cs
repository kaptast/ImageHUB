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
        public async Task SaveImageAsync(IFormFile file, string id, string userName)
        {
            await this.imageStorage.StoreAsync(file);
            this.imageRepository.Save(Path.Combine("images", file.FileName), id, userName);
        }

        public IEnumerable<Post> GetAllImageUrls() => this.imageRepository.GetAll();
    }
}
