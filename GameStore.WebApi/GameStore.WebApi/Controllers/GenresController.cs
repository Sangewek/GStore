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

        /// <returns>Added genre model and result of adding</returns>
        /// <param name="genre"></param>
        /// <response code="201">Returns succeeded created model</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLGenre), 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0)
                return BadRequest("Wrong game model");

            await _genreService.AddAsync(genre);
            return Created(this.RouteData.ToString(), genre);
        }

        /// <returns>Updated genre model and result of adding</returns>
        /// <param name="id"></param>
        /// <param name="genre"></param>
        /// <response code="200">Returns succeeded updated model</response>
        /// <response code="404">Received model is not found in database</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLGenre), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
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

        /// <returns>Certain genre model by id</returns>
        /// <param name="id"></param>
        /// <response code="200">Returns succeeded created model</response>
        /// <response code="404">Model with such id was not found</response>
        [ProducesResponseType(typeof(BLGame), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BLGenre genre = await _genreService.GetAsync(id);
            if (genre?.Name == null)
                return NotFound();
            return Ok(genre);
        }

        /// <returns>All genres models</returns>
        /// <response code="200">Returns all genres collection</response>
        /// <response code="404">Genre models was not found in the database</response>
        [ProducesResponseType(typeof(IEnumerable<BLGenre>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BLGenre> genres = await _genreService.GetAllAsync();
            if (genres?.Count() == 0)
                return NotFound();
            return Ok(genres);
        }

        /// <returns>Result of deleting genre model by id</returns>
        /// <response code="200">Genre model was removed from database</response>
        /// <response code="404">Genre with such id was not found</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || _genreService.GetAsync(id) == null)
                return NotFound();

            await _genreService.DeleteAsync(id);
            return Ok();
        }

        /// <returns>Games of certain genre</returns>
        /// <response code="200">Games of certain genre exist</response>
        /// <response code="404">Genre has no games</response>
        [ProducesResponseType(typeof(IEnumerable<BLGenre>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("{genreId}/games")]
        public async Task<IActionResult> GetGamesByGenre(int genreId)
        {
            if (genreId <= 0)
                return NotFound();

            IEnumerable<BLGame> games = await _genreService.GetGamesByGenreAsync(genreId);
            if (games?.Count() == 0)
                return NotFound();
            return Ok(games);
        }
    }
}