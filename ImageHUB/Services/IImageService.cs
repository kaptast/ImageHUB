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
        Task SaveImageAsync(DatabaseContext context, IFormFile file, Profile owner);

        IEnumerable<Post> GetAllImageUrls(DatabaseContext context, string userID);

        IEnumerable<Post> GetImageUrlsById(DatabaseContext context, string id);
    }
}
