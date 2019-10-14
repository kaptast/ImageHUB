using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Profile
    {
        public IEnumerable<Post> Posts { get; set; }
        public string UserName { get; set; }

        public Profile()
        {
            this.Posts = new List<Post>();
        }
    }
}
