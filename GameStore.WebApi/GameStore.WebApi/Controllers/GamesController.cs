﻿using System;
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
            await _gameService.Add(game);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromBody]BLGame game)
        {
            await _gameService.Update(game);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BLGame> Get(int id)
        {
           return await _gameService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<BLGame>> Get()
        {
            return await _gameService.GetAll();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _gameService.Delete(id);
        }

        [HttpGet]
        [Route("genres/{id}")]
        public async Task<IEnumerable<BLGame>> GetByGenres(int id)
        {
           return await _gameService.GetGamesByGenre(id);
        }
    }
}