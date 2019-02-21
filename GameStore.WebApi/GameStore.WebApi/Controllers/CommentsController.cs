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

        /// <returns>collection of comments to post</returns>
        /// <param name="gameId"></param>
        /// <response code="200">Returns the collection of comments</response>
        /// <response code="404">Comments of certain post was not found</response>
        [ProducesResponseType(typeof(IEnumerable<BLComment>), 200)]
        [ProducesResponseType(404)]
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

        /// <returns>Result of comment creation</returns>
        /// <param name="gameId"></param>
        /// <param name="comment"></param>
        /// <response code="201">Adding a new comment to certain post</response>
        /// <response code="400">Received comment model was not valid</response>
        [ProducesResponseType(typeof(BLComment), 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        [Route("{gameId}")]
        public async Task<IActionResult> Post(int gameId, [FromBody]BLComment comment)
        {
            if (gameId <= 0 || comment == null || comment.Body.Length == 0 || comment.GameId < 0 ||
                comment.Name.Length == 0)
                return BadRequest("Wrong arguments for model creation");

            comment.GameId = gameId;
            await _commentService.AddAsync(comment);
            return Created("/api/comments/" + gameId, comment);
        }

        /// <returns>Result of comment deleting</returns>
        /// <param name="gameId"></param>
        /// <param name="commentId"></param>
        /// <param name="comment"></param>
        /// <response code="201">Adding a new comment to certain post as an answer to anouther comment</response>
        /// <response code="400">Received comment model was not valid</response>
        /// <response code="404">Parent comment was not found</response>
        [ProducesResponseType(typeof(BLComment), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

            return Created("api/comments/" + gameId + "/" + commentId, comment);
        }

        /// <returns>Result of comment deleting</returns>
        /// <param name="id"></param>
        /// <response code="200">Deleted comment from post by post-id</response>
        /// <response code="404">Comment was not found by received id</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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