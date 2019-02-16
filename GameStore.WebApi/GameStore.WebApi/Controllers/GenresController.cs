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
        public async Task<IActionResult> Post([FromBody]BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0)
                return BadRequest("Wrong game model");

            await _genreService.AddAsync(genre);
            return Created(this.RouteData.ToString(), genre);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BLGenre genre)
        {
            if (id <= 0 || _genreService.GetAsync(id) == null)
                return NotFound();
            if (genre == null || genre.Name.Length == 0)
                return NoContent();
            if (genre.Id == 0 && id != 0)
                genre.Id = id;

            await _genreService.UpdateAsync(genre);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BLGenre genre = await _genreService.GetAsync(id);
            if (genre?.Name == null)
                return NotFound();
            return Ok(genre);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BLGenre> genres = await _genreService.GetAllAsync();
            if (genres?.Count() == 0)
                return NotFound();
            return Ok(genres);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || _genreService.GetAsync(id) == null)
                return NotFound();

            await _genreService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{genreId}/games")]
        public async Task<IActionResult> GetGamesByGenre(int genreId)
        {
            if (genreId <= 0)
                return NotFound();

            IEnumerable<BLGame> games = await _genreService.GetGamesByGenreAsync(genreId);
            if (games?.Count() > 0)
                return NotFound();
            return Ok(games);
        }
    }
}