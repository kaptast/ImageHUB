using ImageHUB.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IPostService
    {
        void SavePost(IFormFile file, Profile owner, IEnumerable<string> tags);
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetPostsByUser(string userID);

        IEnumerable<Post> GetPostsByTag(string name);
    }
}
