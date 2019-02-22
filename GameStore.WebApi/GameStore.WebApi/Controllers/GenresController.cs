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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task Post([FromBody]BLGenre genre)
        {
            await _genreService.AddAsync(genre);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromBody]BLGenre genre)
        {
            await _genreService.UpdateAsync(genre);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BLGenre> Get(int id)
        {
            return await _genreService.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<BLGenre>> Get()
        {
            return await _genreService.GetAllAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _genreService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{genreId}/games")]
        public async Task<IEnumerable<BLGame>> GetGamesByGenre(int genreId)
        {
            return await _genreService.GetGamesByGenreAsync(genreId);
        }
    }
}