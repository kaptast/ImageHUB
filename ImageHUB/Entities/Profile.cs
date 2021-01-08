using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ImageHUB.Entities
{
    public class Profile : IEntity
    {
        public int ID { get; set; }
    
        [MaxLength(100)]
        public string UserID { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        [JsonIgnore]
        public ICollection<ProfileFriend> FriendsTo { get; set; }

        [JsonIgnore]
        public ICollection<ProfileFriend> FriendsWith { get; set; }

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
