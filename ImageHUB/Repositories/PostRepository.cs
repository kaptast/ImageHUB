using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageHUB.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext database;

        public PostRepository(DatabaseContext db)
        {
            this.database = db;
        }

        public void Add(Post entity)
        {
            this.database.Posts.Add(entity);
            this.database.SaveChanges();
        }

        public void Delete(Post entity)
        {
            this.database.Posts.Remove(entity);
            this.database.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return this.database.Posts.Include(p => p.Owner).ToList();
        }

        public Post GetByID(string id)
        {
            return this.database.Posts.Include(p => p.Owner).Where(p => p.ID.ToString().Equals(id)).SingleOrDefault();
        }

        public IEnumerable<Post> GetPostsByOwner(string ownerID)
        {
            return this.database.Posts.Include(p => p.Owner).Where(p => p.Owner.UserID.Equals(ownerID)).ToList();
        }

        public void Update(Post entity)
        {
            this.database.Posts.Update(entity);
            this.database.SaveChanges();
        }
    }
}
