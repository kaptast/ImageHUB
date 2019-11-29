using ImageHUB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        IEnumerable<Profile> GetProfilesByName(string userName);
    }
}
