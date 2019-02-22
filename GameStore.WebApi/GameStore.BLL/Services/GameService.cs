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
    public class GameService : BaseService,IGameService
    {
        public GameService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLGame game)
        {
            await UnitOfWork.Games.InsertAsync(AutoMapper.Mapper.Map<BLGame, Game>(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGame game)
        {
            await UnitOfWork.Games.UpdateAsync(AutoMapper.Mapper.Map<BLGame, Game>(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGame> GetAsync(int id)
        {
            Game game = await UnitOfWork.Games.SelectByIdAsync(id);
            return AutoMapper.Mapper.Map<Game,BLGame>(game);
        }

        public async Task<IEnumerable<BLGenre>> GetGenresByGameAsync(int id)
        {
            Game game = await UnitOfWork.Games.GetGameGenres(id);
            List<Genre> genres = new List<Genre>();

            foreach (var genre in game.GameGenres)
                genres.Add(await UnitOfWork.Genres.SelectByIdAsync(genre.GenreId));

            return AutoMapper.Mapper.Map<IEnumerable<Genre>, IEnumerable<BLGenre>>(genres);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByPlatformAsync(int id)
        {
            Platform platform = await UnitOfWork.Platforms.GetPlatformWithGames(id);
            List<Game> games = new List<Game>();

            foreach (var game in platform.GamePlatform)
                games.Add(await UnitOfWork.Games.SelectByIdAsync(game.GameId));

            return AutoMapper.Mapper.Map<IEnumerable<Game>, IEnumerable<BLGame>>(games);
        }

        public async Task<IEnumerable<BLGame>> GetAllAsync()
        {
            return AutoMapper.Mapper.Map < IEnumerable <Game>,IEnumerable<BLGame>>(
                await UnitOfWork.Games.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Games.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
    }
}
