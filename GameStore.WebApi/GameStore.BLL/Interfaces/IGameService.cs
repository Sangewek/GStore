using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        Task Add(BLGame game);
        Task Update(BLGame game);
        Task<BLGame> Get(int id);
        Task<IEnumerable<BLGame>> GetGamesByGenre(int id);
        Task<IEnumerable<BLGame>> GetGamesByPlatform(int id);
        Task<IEnumerable<BLGame>> GetAll();
        Task Delete(int id);
    }
}
