using Forum.Domain.Models;

namespace Forum.Web.Api.Controllers;

public class CreateTopicDto
{
    public string? Subject { get; set; }
    public TopicStatus Status { get; set; }
    public int Likes { get; set; }
    public long? CreatorId { get; set; }
}