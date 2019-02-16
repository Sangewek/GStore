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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BLPublisher publisher)
        {
            if (publisher == null || publisher.Name.Length == 0)
                return BadRequest("Wrong game model");

            await _publisherService.AddAsync(publisher);
            return Created(this.RouteData.ToString(), publisher);
        }

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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BLPublisher publisher = await _publisherService.GetAsync(id);
            if (publisher?.Name == null)
                return NotFound();
            else
                return Ok(publisher);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BLPublisher> publishers = await _publisherService.GetAllAsync();
            if (publishers?.Count() == 0)
                return NotFound();
            else
                return Ok(publishers);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || await _publisherService.GetAsync(id) == null)
                return NotFound();
            await _publisherService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{publisherId}/games")]
        public async Task<IActionResult> GetGamesByPublisher(int publisherId)
        {
            IEnumerable<BLGame> games = await _publisherService.GetGamesByPublisherAsync(publisherId);
            if (games?.Count() == 0)
                return NotFound();
            return Ok(games);
        }
    }
}