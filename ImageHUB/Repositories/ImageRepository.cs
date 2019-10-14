using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ConcurrentBag<Post> imageUrls;

        public ImageRepository()
        {
            this.imageUrls = new ConcurrentBag<Post>();
        }

        public void Save(string path, string id, string userName)
        {
            this.imageUrls.Add(new Post()
            {
                Image = path,
                UserName = userName,
                ID = id
            });
        }

        public IEnumerable<Post> GetAll() => this.imageUrls;

        public IEnumerable<Post> GetById(string id)
        {
            return this.imageUrls.Where(x => x.ID.Equals(id)).ToList();
        }
    }
}
