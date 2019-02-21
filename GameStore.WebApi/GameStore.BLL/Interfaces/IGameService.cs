using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IDisposable
    {
        Task AddAsync(BLGame game);
        Task UpdateAsync(BLGame game);
        Task<BLGame> GetAsync(int id);
        Task<IEnumerable<BLGenre>> GetGenresByGameAsync(int id);
        Task<IEnumerable<BLGame>> GetGamesByPlatformAsync(int id);
        Task<IEnumerable<BLGame>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<string> DownloadGame(int id);
        Task<IEnumerable<BLGame>> NavigateByGames();
    }
}
