using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly IGameService _gameService;

        public GenresController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task Get()
        {

        }
    }
}