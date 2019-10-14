using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Post
    {
        public string UserName { get; set; }
        public string Image { get; set; }
        public string ID { get; set; }

        public bool show { get { return true; } }
    }
}
