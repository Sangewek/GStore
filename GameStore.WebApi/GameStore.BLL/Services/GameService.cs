using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GameService : BaseService<BLGame,Game>,IGameService
    {
        public GameService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0 )
                throw new ArgumentException("Wrong game model");

            await UnitOfWork.Games.InsertAsync(ToDalEntity(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0 || game.Id<=0)
                throw new ArgumentException("Wrong game model");

            await UnitOfWork.Games.UpdateAsync(ToDalEntity(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGame> GetAsync(int id)
        {
            Game game = await UnitOfWork.Games.SelectByIdAsync(id,x=>x.GameGenres,x=>x.GamePlatform);
            return ToBlEntity(game);
        }

        public async Task<IEnumerable<BLGenre>> GetGenresByGameAsync(int id)
        {
            Game game = await UnitOfWork.Games.SelectByIdAsync(id,x=>x.GameGenres);
            List<Genre> genres = new List<Genre>();

            foreach (var genre in game.GameGenres)
                genres.Add(await UnitOfWork.Genres.SelectByIdAsync(genre.GenreId));

            return BaseService<BLGenre,Genre>.ToBlEntity(genres);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByPlatformAsync(int id)
        {
            Platform platform = await UnitOfWork.Platforms.SelectByIdAsync(id, x => x.GamePlatform);
            List<Game> games = new List<Game>();

            foreach (var game in platform.GamePlatform)
                games.Add(await UnitOfWork.Games.SelectByIdAsync(game.GameId));

            return ToBlEntity(games);
        }

        public async Task<IEnumerable<BLGame>> GetAllAsync()
        {
            return ToBlEntity(
                await UnitOfWork.Games.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            if(await UnitOfWork.Games.SelectByIdAsync(id)!=null)
            await UnitOfWork.Games.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
    }
}
