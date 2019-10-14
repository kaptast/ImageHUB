using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ConcurrentBag<string> imageUrls;

        public ImageRepository()
        {
            this.imageUrls = new ConcurrentBag<string>();
        }

        public void Save(string path) => this.imageUrls.Add(path);

        public IEnumerable<string> GetAll() => this.imageUrls;
    }
}
