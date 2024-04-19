using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Services;
using Forum.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TopicController : ControllerBase
{
    private readonly TopicService _topicService;

    public TopicController(TopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<IEnumerable<Topic>> Get()
    {
        return await _topicService.GetAllTopicsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Topic>> Get(long id)
    {
        var topic = await _topicService.GetTopicByIdAsync(id);

        if (topic == null)
        {
            return NotFound();
        }

        return topic;
    }
    public class CreateTopicDto
    {
        public string? Subject { get; set; }
        public TopicStatus Status { get; set; }
        public int Likes { get; set; }
        public long? CreatorId { get; set; }
    }
    [HttpPost]
    public async Task<ActionResult<Topic>> Post([FromBody] CreateTopicDto createTopicDto)
    {
        var topic = new Topic
        {
            Subject = createTopicDto.Subject,
            Status = createTopicDto.Status,
            Likes = createTopicDto.Likes,
            CreatorId = createTopicDto.CreatorId
        };

        var createdTopic = await _topicService.AddTopicAsync(topic);

        return CreatedAtAction(nameof(Get), new { id = createdTopic.Id }, createdTopic);
    }

    public class UpdateTopicDto
    {
        public string? Subject { get; set; }
        public TopicStatus Status { get; set; }
        public int Likes { get; set; }
        public long? CreatorId { get; set; }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateTopicDto updateTopicDto)
    {
        var existingTopic = await _topicService.GetTopicByIdAsync(id);

        if (existingTopic == null)
        {
            return NotFound();
        }

        existingTopic.Subject = updateTopicDto.Subject;
        existingTopic.Status = updateTopicDto.Status;
        existingTopic.Likes = updateTopicDto.Likes;
        existingTopic.CreatorId = updateTopicDto.CreatorId;

        await _topicService.UpdateTopicAsync(existingTopic);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _topicService.DeleteTopicAsync(id);

        return NoContent();
    }
}