using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Post
    {
        public string Image { get; set; }
        public int ID { get; set; }

        public bool show { get { return true; } }

        public Profile Owner {get; set;}
    }
}
