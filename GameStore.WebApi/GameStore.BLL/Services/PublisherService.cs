using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;

namespace GameStore.BLL.Services
{
    public class PublisherService : BaseService<BLPublisher,Publisher>, IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IUnitOfWork unitOfWork,IPublisherRepository publisherRepository) : base(unitOfWork)
        {
            _publisherRepository = publisherRepository;
        }
        public async Task AddAsync(BLPublisher publisher)
        {
            if (publisher == null || publisher.Name.Length == 0)
                throw new ArgumentException("Wrong game model");

            await _publisherRepository.InsertAsync(ToDalEntity(publisher));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLPublisher publisher)
        {
            if (publisher == null || publisher.Name.Length == 0 || publisher.Id<=0)
                throw new ArgumentException("Wrong game model");

            await _publisherRepository.UpdateAsync(ToDalEntity(publisher));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLPublisher> GetAsync(int id)
        {
            Publisher publisher = await _publisherRepository.SelectByIdAsync(id);
            return ToBlEntity(publisher);
        }

        public async Task<IEnumerable<BLPublisher>> GetAllAsync()
        {
            return ToBlEntity(
                await _publisherRepository.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await _publisherRepository.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
        public async Task<IEnumerable<BLGame>> GetGamesByPublisherAsync(int id)
        {
            Publisher publisher = await _publisherRepository.SelectByIdAsync(id,x=>x.Games);
            return BaseService<BLGame,Game>.ToBlEntity(publisher.Games);
        }
    }
}
