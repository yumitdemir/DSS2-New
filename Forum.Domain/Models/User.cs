namespace Forum.Domain.Models
{
    public class User : DomainEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; }
        public ISet<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ISet<Topic> Topics { get; set; } = new HashSet<Topic>();
    }
}
