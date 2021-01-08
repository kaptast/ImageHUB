using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageHUB.Entities
{
    public class Post : IEntity
    {
        public string Image { get; set; }
        public int ID { get; set; }

        [NotMapped]
        public bool show { get { return true; } }

        [JsonIgnore]
        public Profile Owner { get; set; }

        [JsonIgnore]
        public ICollection<PostTag> Tags { get; set; }

        [NotMapped]
        public ProfileDTO OwnerDTO
        {
            get
            {
                return new ProfileDTO(this.Owner);
            }
        }

        [NotMapped]
        public List<string> PostTags {
            get
            {
                var list = new List<string>();
                foreach(var tag in this.Tags){
                    list.Add(tag.Tag.Name);
                }

                return list;
            }
        }
    }
}
