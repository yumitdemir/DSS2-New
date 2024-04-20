using Forum.Domain.Models;

namespace Forum.Application.Dto;

public class UpdateTopicDto
{
    public long? Id { get; set; }
    public string? Subject { get; set; }
    public TopicStatus Status { get; set; }
    public int Likes { get; set; }
    public long? CreatorId { get; set; }
}