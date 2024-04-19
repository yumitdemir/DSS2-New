using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Application.Services;
using Forum.Domain.Models;

namespace Forum.Web.Api.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        public class CreateCommentDto
        {
            public string? Text { get; set; }
            public CommentStatus Status { get; set; }
            public int Likes { get; set; }
            public long? CreatorId { get; set; }
            public long? TopicId { get; set; }
        }

        public class UpdateCommentDto
        {
            public string? Text { get; set; }
            public CommentStatus Status { get; set; }
            public int Likes { get; set; }
        }
        [HttpGet]
        public async Task<IEnumerable<Comment>> Get()
        {
            return await _commentService.GetAllCommentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(long id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Post([FromBody] CreateCommentDto createCommentDto)
        {
            var comment = new Comment
            {
                Text = createCommentDto.Text,
                Status = createCommentDto.Status,
                Likes = createCommentDto.Likes,
                CreatorId = createCommentDto.CreatorId,
                TopicId = createCommentDto.TopicId
            };

            var createdComment = await _commentService.AddCommentAsync(comment);

            return CreatedAtAction(nameof(Get), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var existingComment = await _commentService.GetCommentByIdAsync(id);

            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Text = updateCommentDto.Text;
            existingComment.Status = updateCommentDto.Status;
            existingComment.Likes = updateCommentDto.Likes;

            await _commentService.UpdateCommentAsync(existingComment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _commentService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}