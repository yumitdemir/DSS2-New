using Forum.Domain.Models;

public class CreateCommentDto
{
    public string? Text { get; set; }
    public CommentStatus Status { get; set; }
    public int Likes { get; set; }
    public long? CreatorId { get; set; }
    public long? TopicId { get; set; }
}