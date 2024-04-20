using System.Collections.Generic;
using Forum.Application.Dto;
using Forum.Domain.Models;

namespace Forum.Infrastructure;

public class TopicDetailDto
{
    public long? Id { get; set; }
    public string? Subject { get; set; }
    public TopicStatus Status { get; set; }
    public int Likes { get; set; }
    public long? CreatorId { get; set; }
    public UserDetailsDto Creator { get; set; }
    public IEnumerable<CommentDetailDto> Comments { get; set; }
}