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
        private readonly ITagRepository tagRepository;
        private readonly IImageStorage storage;

        public PostService(IPostRepository repo, IImageStorage storage, ITagRepository tagRepo)
        {
            this.repository = repo;
            this.storage = storage;
            this.tagRepository = tagRepo;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Post> GetPostsByTag(string name)
        {
            return this.tagRepository.GetPostsByTag(name);
        }

        public IEnumerable<Post> GetPostsByUser(string userID)
        {
            return this.repository.GetPostsByOwner(userID);
        }

        public async Task SavePostAsync(IFormFile file, Profile owner, IEnumerable<string> tags)
        {
            var base64Image = this.storage.StoreBase64(file);
            var post = new Post(){
                Image = base64Image,//Path.Combine("img", file.FileName),
                Owner = owner
            };
            post.Tags = this.GenerateTagConnections(post, tags);

            this.repository.Add(post);
        }

        private List<PostTag> GenerateTagConnections(Post post, IEnumerable<string> tags)
        {
            var postTags = new List<PostTag>();

            foreach(var tag in tags)
            {
                var dbTag = this.tagRepository.GetByName(tag);

                if (dbTag == null)
                {
                    dbTag = new Tag(){
                        Name = tag
                    };
                    this.tagRepository.Add(dbTag);
                    dbTag = this.tagRepository.GetByName(tag); // refresh from database
                }

                var postTag = new PostTag();
                postTag.Post = post;
                postTag.Tag = dbTag;
                postTags.Add(postTag);
            }

            return postTags;
        }
    }
}
