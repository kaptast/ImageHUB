using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IImageService
    {
        Task SaveImageAsync(IFormFile file, Profile owner);

        IEnumerable<Post> GetAllImageUrls(string userID);

        IEnumerable<Post> GetImageUrlsById(string id);
    }
}
