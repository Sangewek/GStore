using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService: IDisposable
    {
        Task AddAsync(BLPublisher publisher);
        Task UpdateAsync(BLPublisher publisher);
        Task<BLPublisher> GetAsync(int id);
        Task<IEnumerable<BLPublisher>> GetAllAsync();
        Task<IEnumerable<BLGame>> GetGamesByPublisherAsync(int id);
        Task DeleteAsync(int id);
    }
}
