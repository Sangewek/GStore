using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        /// <returns>Added game model and result of adding</returns>
        /// <param name="game"></param>
        /// <response code="201">Returns succeeded created model</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLGame), 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0)
                return BadRequest("Wrong game model");

            await _gameService.AddAsync(game);
            return Created("api/games/", game);
        }

        /// <returns>Updated game model and result of adding</returns>
        /// <param name="id"></param>
        /// <param name="game"></param>
        /// <response code="200">Returns succeeded updated model</response>
        /// <response code="404">Received model is not found in database</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLGame), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BLGame game)
        {
            if (id <= 0)
                return NotFound();
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0)
                return NoContent();
            if (game.Id == 0 && id != 0)
                game.Id = id;

            await _gameService.UpdateAsync(game);
            return Ok();
        }

        /// <returns>Certain game model by id</returns>
        /// <param name="id"></param>
        /// <response code="200">Returns succeeded created model</response>
        /// <response code="404">Model with such id was not found</response>
        [ProducesResponseType(typeof(BLGame), 200)]
        [ProducesResponseType(404)]
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

        /// <returns>All game models</returns>
        /// <response code="200">Returns collection of all game models </response>
        /// <response code="404">Game models was not found in the database</response>
        [ProducesResponseType(typeof(IEnumerable<BLGame>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<BLGame> games = await _gameService.GetAllAsync();
            if (games?.Count() == 0)
                return NotFound();
            else
                return Ok(games);
        }

        /// <returns>Result of deleting game model by id</returns>
        /// <response code="200">Game model was removed from database</response>
        /// <response code="404">Game with such id was not found</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || await _gameService.GetAsync(id) == null)
                return NotFound();

            await _gameService.DeleteAsync(id);
            return Ok();
        }

        /// <returns>Genres of certain game</returns>
        /// <response code="200">Genres of certain game exist</response>
        /// <response code="404">Game has no genres</response>
        [ProducesResponseType(typeof(IEnumerable<BLGenre>), 200)]
        [ProducesResponseType(404)]
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

        /// <returns>Game models witch has chose platform</returns>
        /// <response code="200">Games with such platform id exist</response>
        /// <response code="404">Games with such platform id was not found</response>
        [ProducesResponseType(typeof(IEnumerable<BLGame>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("platform/{platformId}")]
        public async Task<IActionResult> GetByPlatform(int platformId)
        {
            IEnumerable<BLGame> games = await _gameService.GetGamesByPlatformAsync(platformId);
            if (games?.Count() == 0)
                return NotFound();
            else
                return Ok(games);
        }

        /// <returns>Game models witch has chose platform</returns>
        /// <response code="200">Games with such platform id exist</response>
        /// <response code="404">Games with such id was not found</response>
        [ProducesResponseType(typeof(HttpResponse), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadGame(int id)
        {
            HttpResponse response = HttpContext.Response;
            string gameName = await _gameService.DownloadGame(id);
            if (gameName == null || gameName.Length < 3)
                return NotFound();
            response.Clear();
            response.ContentType = "application/octet-stream";
            response.Headers.Add("content-disposition", "attachment;filename=" + $"{gameName}.bin");
            await response.SendFileAsync("Files/Game.bin");
            return Ok(response);
        }


        /// <returns>All game models</returns>
        /// <response code="200">Returns collection of all game models </response>
        /// <response code="404">Game models was not found in the database</response>
        [ProducesResponseType(typeof(IEnumerable<BLGame>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("navigation")]
        public async Task<IActionResult> NavigateByGames()
        {
            IEnumerable<BLGame> games = await _gameService.NavigateByGames();
            if (games?.Count() == 0)
                return NotFound();
            else
                return Ok(games);
        }
    }
}