using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class GenreService : BaseService, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLGenre genre)
        {
            await UnitOfWork.Genres.InsertAsync(AutoMapper.Mapper.Map<BLGenre, Genre>(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGenre genre)
        {
            await UnitOfWork.Genres.UpdateAsync(AutoMapper.Mapper.Map<BLGenre, Genre>(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGenre> GetAsync(int id)
        {
            Genre genre = await UnitOfWork.Genres.SelectByIdAsync(id);
            return AutoMapper.Mapper.Map<Genre, BLGenre>(genre);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByGenreAsync(int id)
        {
            Genre genre = await UnitOfWork.Genres.SelectByIdAsync(id,x=>x.GameGenres);
            List<Game> games = new List<Game>();

            foreach (var game in genre.GameGenres)
                games.Add(await UnitOfWork.Games.SelectByIdAsync(game.GameId));

            return AutoMapper.Mapper.Map<IEnumerable<Game>, IEnumerable<BLGame>>(games);
        }

        public async Task<IEnumerable<BLGenre>> GetAllAsync()
        {
            return AutoMapper.Mapper.Map<IEnumerable<Genre>, IEnumerable<BLGenre>>(
                await UnitOfWork.Genres.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Genres.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
    }

}
