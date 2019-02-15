using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;

namespace GameStore.BLL.Services
{
    public class PublisherService : BaseService, IPublisherService
    {
        public PublisherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task AddAsync(BLPublisher Publisher)
        {
            await UnitOfWork.Publishers.InsertAsync(AutoMapper.Mapper.Map<BLPublisher, Publisher>(Publisher));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLPublisher Publisher)
        {
            await UnitOfWork.Publishers.UpdateAsync(AutoMapper.Mapper.Map<BLPublisher, Publisher>(Publisher));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLPublisher> GetAsync(int id)
        {
            Publisher Publisher = await UnitOfWork.Publishers.SelectByIdAsync(id);
            return AutoMapper.Mapper.Map<Publisher, BLPublisher>(Publisher);
        }

        public async Task<IEnumerable<BLPublisher>> GetAllAsync()
        {
            return AutoMapper.Mapper.Map<IEnumerable<Publisher>, IEnumerable<BLPublisher>>(
                await UnitOfWork.Publishers.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Publishers.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
        public async Task<IEnumerable<BLGame>> GetGamesByPublisherAsync(int id)
        {
            Publisher publisher = await UnitOfWork.Publishers.SelectByIdAsync(id,x=>x.Games);
            return AutoMapper.Mapper.Map<IEnumerable<Game>,IEnumerable<BLGame>>(publisher.Games);
        }
    }
}
