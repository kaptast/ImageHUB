using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;

namespace ImageHUB.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly IImageStorage storage;

        public PostService(IPostRepository repo, IImageStorage storage)
        {
            this.repository = repo;
            this.storage = storage;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Post> GetFeedPostsByUser(string userID)
        {
            // TODO: append friends posts
            return this.GetPostsByUser(userID);
        }

        public IEnumerable<Post> GetPostsByUser(string userID)
        {
            return this.repository.GetPostsByOwner(userID);
        }

        public async Task SavePostAsync(IFormFile file, Profile owner)
        {
            var post = new Post(){
                Image = Path.Combine("img", file.FileName),
                Owner = owner
            };

            this.repository.Add(post);

            await this.storage.StoreAsync(file);
        }
    }
}
