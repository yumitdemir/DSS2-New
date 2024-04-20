using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Services;
using Forum.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Forum.Application.Dto;
using Forum.Infrastructure;

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
    public async Task<IEnumerable<TopicDetailDto>> Get()
    {
        return await _topicService.GetAllTopicsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicDetailDto>> Get(long id)
    {
        var topic = await _topicService.GetTopicByIdAsync(id);

        if (topic == null)
        {
            return NotFound();
        }

        return topic;
    }

    [HttpPost]
    public async Task<ActionResult<TopicDetailDto>> Post([FromBody] CreateTopicDto createTopicDto)
    {
        var newTopic = await _topicService.AddTopicAsync(createTopicDto);

        return CreatedAtAction(nameof(Get), new { id = newTopic.Id }, newTopic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateTopicDto updateTopicDto)
    {
        var updatedTopic = await _topicService.UpdateTopicAsync(id, updateTopicDto);

        if (updatedTopic == null)
        {
            return NotFound();
        }

        return Ok(updatedTopic);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _topicService.DeleteTopicAsync(id);

        return NoContent();
    }
}