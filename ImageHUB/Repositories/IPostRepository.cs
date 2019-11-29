using ImageHUB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPostsByOwner(string ownerID);
    }
}
