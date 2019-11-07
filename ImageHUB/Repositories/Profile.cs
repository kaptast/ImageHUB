using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Profile
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Profile> Friends { get; set; }

        public string ID {get; set;}
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }
    }
}
