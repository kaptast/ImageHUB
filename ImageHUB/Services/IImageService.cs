using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IImageService
    {
        Task SaveImageAsync(IFormFile file);

        IEnumerable<string> GetAllImageUrls();
    }
}
