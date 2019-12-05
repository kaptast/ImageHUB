using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageHUB.Repositories
{
    public class TagRepository : ITagRepository
    {
        private DatabaseContext database;

        public TagRepository(DatabaseContext db)
        {
            this.database = db;
        }
        public void Add(Tag entity)
        {
            this.database.Tags.Add(entity);
            this.database.SaveChanges();
        }

        public void Delete(Tag entity)
        {
            this.database.Tags.Remove(entity);
            this.database.SaveChanges();
        }

        public IEnumerable<Tag> GetAll()
        {
            return this.database.Tags.ToList();
        }

        public Tag GetByID(string id)
        {
            return this.database.Tags.Where(x => x.ID.Equals(id)).SingleOrDefault();
        }

        public Tag GetByName(string name)
        {
            return this.database.Tags.Where(x => x.Name.Equals(name)).SingleOrDefault();
        }

        public IEnumerable<Post> GetPostsByTag(string tag)
        {
            throw new NotImplementedException();
            //return this.database.Tags.Include(t => t.Posts).ThenInclude(pt => pt.Post).Where(t => t.Name.Equals(tag)).ToList();
        }

        public void Update(Tag entity)
        {
            this.database.Tags.Update(entity);
            this.database.SaveChanges();
        }
    }
}