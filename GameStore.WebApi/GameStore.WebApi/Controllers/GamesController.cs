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
        public async Task Post([FromBody]BLGame game)
        {
            await _gameService.AddAsync(game);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromBody]BLGame game)
        {
            await _gameService.UpdateAsync(game);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BLGame> Get(int id)
        {
           return await _gameService.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<BLGame>> Get()
        {
            return await _gameService.GetAllAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _gameService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}/genres")]
        public async Task<IEnumerable<BLGenre>> GetByGenres(int id)
        {
           return await _gameService.GetGenresByGameAsync(id);
        }

        [HttpGet]
        [Route("{id}/platforms")]
        public async Task<IEnumerable<BLGame>> GetByPlatform(int id)
        {
            return await _gameService.GetGamesByPlatformAsync(id);
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
            return File(memory, "file/bin", Path.GetFileName(path));
        }
    }
}