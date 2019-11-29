using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageHUB.Entities
{
    public class Profile : IEntity
    {
        public int ID { get; set; }
    
        [MaxLength(100)]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }

    public class ProfileDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }

        public ProfileDTO(Profile profile)
        {
            this.ID = profile.UserID;
            this.UserName = profile.UserName;
        }
    }
}
