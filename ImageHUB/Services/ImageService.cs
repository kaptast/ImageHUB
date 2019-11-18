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

        public ImageService(IImageStorage imageStorage)
        {
            this.imageStorage = imageStorage;
        }
        public async Task SaveImageAsync(DatabaseContext context, IFormFile file, Profile owner)
        {
            await this.imageStorage.StoreAsync(file);
            context.SaveImage(Path.Combine("img", file.FileName), owner);
        }

        public IEnumerable<Post> GetAllImageUrls(DatabaseContext context, string userID) => context.GetAllPosts(userID);

        public IEnumerable<Post> GetImageUrlsById(DatabaseContext context, string id) => context.GetPostByUserID(id);

    }
}
