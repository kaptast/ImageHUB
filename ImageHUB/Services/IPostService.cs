using ImageHUB.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IPostService
    {
        Task SavePostAsync(IFormFile file, Profile owner);
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetFeedPostsByUser(string userID);
        IEnumerable<Post> GetPostsByUser(string userID);
    }
}
