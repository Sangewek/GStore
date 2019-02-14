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
            return await _commentService.GetCommentsForPost(gameId);
        }

        [HttpPost]
        public async Task Post([FromBody]BLComment comment)
        {
            await _commentService.Add(comment);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
             await _commentService.Delete(id);
        }

    }
}