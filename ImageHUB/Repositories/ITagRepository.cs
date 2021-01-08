using System.Collections.Generic;
using ImageHUB.Entities;

namespace ImageHUB.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag GetByName(string name);
        IEnumerable<Post> GetPostsByTag(string name);
    }
}
