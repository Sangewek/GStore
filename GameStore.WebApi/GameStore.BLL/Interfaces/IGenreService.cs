using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService: IService
    {
        Task AddAsync(BLGenre genre);
        Task UpdateAsync(BLGenre genre);
        Task<BLGenre> GetAsync(int id);
        Task<IEnumerable<BLGame>> GetGamesByGenreAsync(int id);
        Task<IEnumerable<BLGenre>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
