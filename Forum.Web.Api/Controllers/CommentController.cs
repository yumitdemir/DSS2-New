using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Infrastructure;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(CommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentDetailDto>> Get()
    {
        return await _commentService.GetAllCommentsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDetailDto>> Get(long id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDetailDto>> Post(CreateCommentDto createCommentDto)
    {
        var newComment = await _commentService.AddCommentAsync(createCommentDto);

        return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        var updatedComment = await _commentService.UpdateCommentAsync(id, updateCommentDto);

        if (updatedComment == null)
        {
            return NotFound();
        }

        return Ok(updatedComment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _commentService.DeleteCommentAsync(id);

        return NoContent();
    }
}