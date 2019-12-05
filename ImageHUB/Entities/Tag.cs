using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageHUB.Entities
{
    public class Tag : IEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<PostTag> Posts { get; set; }
    }

    public class PostTag : IEntity
    {
        public int ID { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}
