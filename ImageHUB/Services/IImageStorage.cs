using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageHUB.Services
{
    public interface IImageStorage
    {
        Task StoreAsync(IFormFile file);
    }
}