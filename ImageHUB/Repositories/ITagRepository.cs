using ImageHUB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Post> GetPostsByTag(string tag);
    }
}
