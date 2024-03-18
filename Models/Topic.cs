using System.Collections.Generic;

namespace Forum.Domain.Models
{
    public class Topic : DomainEntity
    {
        public User? Creator { get; set; }
        public long? CreatorId { get; set; }
        public string? Subject { get; set; }
        public TopicStatus Status { get; set; }
        public int Likes { get; set; }
        public ISet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
