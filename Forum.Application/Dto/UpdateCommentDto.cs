using Forum.Domain.Models;

public class UpdateCommentDto
{
    public long? Id { get; set; }
    public string? Text { get; set; }
    public CommentStatus Status { get; set; }
    public int Likes { get; set; }
}