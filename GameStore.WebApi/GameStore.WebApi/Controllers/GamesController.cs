using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0 )
                return BadRequest("Wrong game model");

            await _gameService.AddAsync(game);
            return Created("api/games/", game);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BLGame game)
        {
            if (id <= 0)
                return NotFound();
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0 )
                return NoContent();
            if (game.Id == 0 && id != 0)
                game.Id = id;

            await _gameService.UpdateAsync(game);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BLGame game = await _gameService.GetAsync(id);
            if (game?.Name == null)
                return NotFound();
            else
                return Ok(game);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<BLGame> games = await _gameService.GetAllAsync();
            if (games?.Count()==0)
                return NotFound();
            else
                return Ok(games);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || await _gameService.GetAsync(id) == null)
                return NotFound();

            await _gameService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/genres")]
        public async Task<IActionResult> GetGenres(int id)
        {
            IEnumerable<BLGenre> genres = await _gameService.GetGenresByGameAsync(id);
            if (genres?.Count() == 0)
                return NotFound();
            else
                return Ok(genres);
        }

        [HttpGet]
        [Route("{id}/platforms")]
        public async Task<IActionResult> GetByPlatform(int id)
        {
            IEnumerable<BLGame> games = await _gameService.GetGamesByPlatformAsync(id);
            if (games?.Count() == 0)
                return NotFound();
            else
                return Ok(games);
        }

        [HttpGet]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadGame()
        {
            string path = "Files/Game.bin";

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return Ok(File(memory, "file/bin", Path.GetFileName(path)));
        }
    }
}