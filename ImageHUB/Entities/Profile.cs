using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Entities
{
    public class Profile : IEntity
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
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
