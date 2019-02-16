using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{gameId}")]
        public async Task<IActionResult> Get(int gameId)
        {
            IEnumerable<BLComment> comments = await _commentService.GetCommentsForPostAsync(gameId);
            if (comments == null || !comments.Any())
                return NotFound();
            else
                return Ok(comments);
        }

        [HttpPost]
        [Route("{gameId}")]
        public async Task<IActionResult> Post(int gameId, [FromBody]BLComment comment)
        {
            if (gameId <= 0 || comment == null || comment.Body.Length == 0 || comment.GameId < 0 ||
                comment.Name.Length == 0)
                return BadRequest("Wrong arguments for model creation");

            comment.GameId = gameId;
            await _commentService.AddAsync(comment);
            return Created("/api/comments/"+gameId, comment);
        }

        [HttpPost]
        [Route("{gameId}/{commentId}")]
        public async Task<IActionResult> AnswerToComment(int gameId, int commentId, [FromBody]BLComment comment)
        {
            if (gameId <= 0 || comment == null || comment.Body.Length == 0 || comment.GameId < 0 ||
                comment.Name.Length == 0 || commentId <= 0)
                return BadRequest("Wrong arguments for model creation");

            comment.GameId = gameId;
            if ((await _commentService.GetById(commentId))?.Body == null)
                return NotFound();

            comment.ParentCommentId = commentId;
            await _commentService.AddAsync(comment);

            return Created("api/comments/"+gameId+"/"+commentId, comment);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || await _commentService.GetById(id) == null)
                return NotFound();

            await _commentService.DeleteAsync(id);
            return Ok();
        }

    }
}