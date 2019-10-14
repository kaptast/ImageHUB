using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    
        public interface IImageRepository
        {
            void Save(string path);

            IEnumerable<string> GetAll();
        }
    
}
