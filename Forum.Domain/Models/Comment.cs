namespace Forum.Domain.Models
{
    public class Comment : DomainEntity
    {
        public int Likes { get; set; }
        public string? Text { get; set; }
        public CommentStatus Status { get; set; }
        public User? Creator { get; set; }
        public long? CreatorId { get; set; }
        public Topic? Topic { get; set; }
    }
}
