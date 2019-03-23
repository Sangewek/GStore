using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        /// <returns>Added publisher model and result of adding</returns>
        /// <param name="publisher"></param>
        /// <response code="201">Returns succeeded created model</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLPublisher), 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BLPublisher publisher)
        {
            if (publisher == null || publisher.Name.Length == 0)
                return BadRequest("Wrong game model");

            await _publisherService.AddAsync(publisher);
            return Created(this.RouteData.ToString(), publisher);
        }

        /// <returns>Updated publisher model and result of adding</returns>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        /// <response code="200">Returns succeeded updated model</response>
        /// <response code="404">Received model is not found in database</response>
        /// <response code="400">Received model is not valid</response>
        [ProducesResponseType(typeof(BLPublisher), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BLPublisher publisher)
        {
            if (id == 0)
                return NotFound();
            if (publisher == null || publisher.Name.Length == 0)
                return NoContent();
            if (publisher.Id == 0 && id != 0)
                publisher.Id = id;

            await _publisherService.UpdateAsync(publisher);
            return Ok();
        }

        /// <returns>Certain publisher model by id</returns>
        /// <param name="id"></param>
        /// <response code="200">Returns succeeded created model</response>
        /// <response code="404">Model with such id was not found</response>
        [ProducesResponseType(typeof(BLPublisher), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BLPublisher publisher = await _publisherService.GetAsync(id);
            if (publisher?.Name == null)
                return NotFound();
            else
                return Ok(AutoMapper.Mapper.Map<BLPublisher, PublisherViewModel>(publisher));
        }

        /// <returns>All publishers models</returns>
        /// <response code="200">Returns all publishers collection</response>
        /// <response code="404">Genre models was not found in the database</response>
        [ProducesResponseType(typeof(IEnumerable<BLPublisher>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BLPublisher> publishers = await _publisherService.GetAllAsync();
            if (publishers?.Count() == 0)
                return NotFound();
            else
                return Ok(AutoMapper.Mapper.Map<IEnumerable<BLPublisher>, IEnumerable<PublisherViewModel>>(publishers));
        }

        /// <returns>Result of deleting publisher model by id</returns>
        /// <response code="200">Publisher model was removed from database</response>
        /// <response code="404">Publisher with such id was not found</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || await _publisherService.GetAsync(id) == null)
                return NotFound();
            await _publisherService.DeleteAsync(id);
            return Ok();
        }

        /// <returns>Games of certain publisher</returns>
        /// <response code="200">Games of certain publisher exist</response>
        /// <response code="404">Publisher has no games</response>
        [ProducesResponseType(typeof(IEnumerable<BLPublisher>), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("{publisherId}/games")]
        public async Task<IActionResult> GetGamesByPublisher(int publisherId)
        {
            IEnumerable<BLGame> games = await _publisherService.GetGamesByPublisherAsync(publisherId);
            if (games?.Count() == 0)
                return NotFound();
            return Ok(AutoMapper.Mapper.Map<IEnumerable<BLGame>, IEnumerable<GameViewModel>>(games));
        }
    }
}

