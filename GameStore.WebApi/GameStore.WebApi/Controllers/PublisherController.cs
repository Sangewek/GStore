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
        public async Task Post([FromBody]BLPublisher publisher)
        {
            await _publisherService.AddAsync(publisher);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromBody]BLPublisher publisher)
        {
            await _publisherService.UpdateAsync(publisher);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BLPublisher> Get(int id)
        {
            return await _publisherService.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<BLPublisher>> Get()
        {
            return await _publisherService.GetAllAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _publisherService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{publisherId}/games")]
        public async Task<IEnumerable<BLGame>> GetGamesByPublisher(int publisherId)
        {
            return await _publisherService.GetGamesByPublisherAsync(publisherId);
        }
    }
}