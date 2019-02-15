using System;
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
        public async Task<IEnumerable<BLComment>> Get(int gameId)
        {
            return await _commentService.GetCommentsForPostAsync(gameId);
        }

        [HttpPost]
        [Route("{gameId}")]
        public async Task Post(int gameId, [FromBody]BLComment comment)
        {
            comment.GameId = gameId;
            await _commentService.AddAsync(comment);
        }

        [HttpPost]
        [Route("{gameId}/{commentId}")]
        public async Task AnswerToComment(int gameId, int commentId, [FromBody]BLComment comment)
        {
            comment.GameId = gameId;
            comment.ParentCommentId = commentId;
            await _commentService.AddAsync(comment);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _commentService.DeleteAsync(id);
        }

    }
}