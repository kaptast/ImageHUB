using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    
        public interface IImageRepository
        {
            void Save(string path, string id, string userName);

            IEnumerable<Post> GetAll();

        IEnumerable<Post> GetById(string id);
        }
    
}
