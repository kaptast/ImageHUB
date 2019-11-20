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
        private readonly IImageStorage imageStorage;
        private readonly IRepository repository;

        public ImageService(IImageStorage imageStorage, IRepository repo)
        {
            this.imageStorage = imageStorage;
            this.repository = repo;
        }
        public async Task SaveImageAsync(IFormFile file, Profile owner)
        {
            await this.imageStorage.StoreAsync(file);
            repository.SaveImage(Path.Combine("img", file.FileName), owner);
        }

        public IEnumerable<Post> GetAllImageUrls(string userID) => repository.GetAllPosts(userID);

        public IEnumerable<Post> GetImageUrlsById(string id) => repository.GetPostByUserID(id);

    }
}
