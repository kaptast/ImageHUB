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

        [NotMapped]
        public ProfileDTO OwnerDTO
        {
            get
            {
                return new ProfileDTO(this.Owner);
            }
        }

    }
}
