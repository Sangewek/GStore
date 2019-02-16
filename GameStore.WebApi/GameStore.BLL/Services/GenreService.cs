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
    public class GenreService : BaseService<BLGenre,Genre>, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0 )
                throw new ArgumentException("Wrong game model");

            await UnitOfWork.Genres.InsertAsync(ToDalEntity(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0 || genre.Id<=0)
                throw new ArgumentException("Wrong game model");

            await UnitOfWork.Genres.UpdateAsync(ToDalEntity(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGenre> GetAsync(int id)
        {
            Genre genre = await UnitOfWork.Genres.SelectByIdAsync(id);
            return ToBlEntity(genre);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByGenreAsync(int id)
        {
            Genre genre = await UnitOfWork.Genres.SelectByIdAsync(id,x=>x.GameGenres);
            List<Game> games = new List<Game>();

            foreach (var game in genre.GameGenres)
                games.Add(await UnitOfWork.Games.SelectByIdAsync(game.GameId));

            return BaseService<BLGame,Game>.ToBlEntity(games);
        }

        public async Task<IEnumerable<BLGenre>> GetAllAsync()
        {
            return ToBlEntity(
                await UnitOfWork.Genres.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Genres.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
    }

}
