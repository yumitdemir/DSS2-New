using Forum.Application.Dto;
using Forum.Domain.Models;

namespace Forum.Infrastructure;

public class CommentDetailDto
{
    public long? Id { get; set; }
    public string? Text { get; set; }
    public CommentStatus Status { get; set; }
    public int Likes { get; set; }
    public long? CreatorId { get; set; }
    public UserDetailsDto Creator { get; set; }
    public long? TopicId { get; set; }
}