using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Entities
{
    public class ProfileFriend : IEntity
    {
        public int ID { get; set; }

        public int ProfileID { get; set; }
        public Profile Profile { get; set; }
        public int FriendID { get; set; }
        public Profile Friend { get; set; }

        public bool Accepted { get; set; }
    }
}
