//#define BACKEND_VALIDATION 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;

namespace ImageHUB.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly ITagRepository tagRepository;
        private readonly IImageStorage storage;
        public IConfiguration Configuration { get; }

        public PostService(IPostRepository repo, IImageStorage storage, ITagRepository tagRepo, IConfiguration configuration)
        {
            this.repository = repo;
            this.storage = storage;
            this.tagRepository = tagRepo;
            Configuration = configuration;
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

        public void SavePost(IFormFile file, Profile owner, IEnumerable<string> tags)
        {
            var base64Image = this.storage.StoreBase64(file);
            var post = new Post()
            {
                Image = base64Image,//Path.Combine("img", file.FileName),
                Owner = owner
            };

#if BACKEND_VALIDATION
            IEnumerable<string> newTags;
            if (this.AnalyzeFormImage(file, out newTags))
            {
                post.Tags = this.GenerateTagConnections(post, newTags);
                this.repository.Add(post);
            }
#else
            post.Tags = this.GenerateTagConnections(post, tags);
            this.repository.Add(post);
#endif
        }

        private List<PostTag> GenerateTagConnections(Post post, IEnumerable<string> tags)
        {
            var postTags = new List<PostTag>();

            foreach (var tag in tags)
            {
                var dbTag = this.tagRepository.GetByName(tag);

                if (dbTag == null)
                {
                    dbTag = new Tag()
                    {
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

        private bool AnalyzeFormImage(IFormFile file, out IEnumerable<string> tags)
        {
            var client = Authenticate();

            tags = AnalyzeImage(client, file).Result;

            if (tags.Contains("train") || tags.Contains("trains"))
            {
                return false;
            }

            return true;

        }
        private ComputerVisionClient Authenticate()
        {
            string key = this.Configuration["CognitiveServices:Key"];
            string endpoint = "https://stepimagehubvision.cognitiveservices.azure.com/";

            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        public async Task<IEnumerable<string>> AnalyzeImage(ComputerVisionClient client, IFormFile file)
        {
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
            {
                VisualFeatureTypes.Tags
            };

            ImageAnalysis results = await client.AnalyzeImageInStreamAsync(file.OpenReadStream(), features);

            return results.Tags.Select(t => t.Name).ToList();
        }

    }
}
