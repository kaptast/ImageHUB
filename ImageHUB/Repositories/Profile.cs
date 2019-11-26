using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Profile
    {
        public IEnumerable<Post> Posts { get; set; }

        public ICollection<ProfileFriend> FriendsTo { get; set; }

        public ICollection<ProfileFriend> FriendsWith { get; set; }

        public int ID { get; set; }

        public string UserID { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }

        [NotMapped]
        public IEnumerable<Profile> Friends { get; set; }

        [NotMapped]
        public bool ShowFriendButton { get; set; }

        [NotMapped]
        public FriendStatus Status { get; set; }
    }

    public enum FriendStatus
    {
        NotFriends = 0,
        Pending = 1,
        Friends = 2,
        Waiting = 3
    }

    public class ProfileFriend
    {
        public int ProfileID { get; set; }
        public Profile Profile { get; set; }
        public int FriendID { get; set; }
        public Profile Friend { get; set; }

        public bool Accepted { get; set; }

    }
}
