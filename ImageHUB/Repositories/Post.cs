using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Post
    {
        public string Image { get; set; }
        public int ID { get; set; }

        public bool show { get { return true; } }

        [JsonIgnore]
        public Profile Owner { get; set; }

        [NotMapped]
        public ProfileDTO OwnerDTO
        {
            get
            {
                return new ProfileDTO()
                {
                    ID = this.Owner.ID,
                    UserName = this.Owner.UserName,
                    Avatar = this.Owner.ID,
                };
            }
            set { }
        }
    }

    public class ProfileDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }
    }
}
