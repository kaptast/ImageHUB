using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Profile
    {
        public IEnumerable<Post> Posts { get; set; }
        public ICollection<ProfileFriend> FriendsTo { get; set; }
        public ICollection<ProfileFriend> FriendsWith { get; set; }

        public string ID {get; set;}
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }
    }

    public class ProfileFriend
    {
        public string ProfileID { get; set; }
        public Profile Profile { get; set; }
        public string FriendID { get; set; }
        public Profile Friend { get; set; }

        public bool Accepted { get; set; }

    }
}
