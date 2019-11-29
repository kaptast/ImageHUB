using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;

namespace ImageHUB.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DatabaseContext database;

        public ProfileRepository(DatabaseContext db)
        {
            this.database = db;
        }

        public void Add(Profile entity)
        {
            try
            {
                this.database.Profiles.Add(entity);
                this.database.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void Delete(Profile entity)
        {
            this.database.Remove(entity);
            this.database.SaveChanges();
        }

        public IEnumerable<Profile> GetAll()
        {
            return this.database.Profiles.ToList();
        }

        public Profile GetByID(string id)
        {
            return this.database.Profiles.Where(p => p.UserID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<Profile> GetProfilesByName(string userName)
        {
            return this.database.Profiles.Where(p => p.UserName.ToLower().Contains(userName.ToLower())).ToList();
        }

        public void Update(Profile entity)
        {
            this.database.Profiles.Update(entity);
            this.database.SaveChanges();
        }
    }
}
