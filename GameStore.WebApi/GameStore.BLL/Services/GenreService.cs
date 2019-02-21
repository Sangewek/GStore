using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Interfaces.Repositories;

namespace GameStore.BLL.Services
{
    public class GenreService : BaseService<BLGenre,Genre>, IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IUnitOfWork unitOfWork,IGenreRepository genreRepository) : base(unitOfWork)
        {
            _genreRepository = genreRepository;
        }

        public async Task AddAsync(BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0 )
                throw new ArgumentException("Wrong game model");

            await _genreRepository.InsertAsync(ToDalEntity(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGenre genre)
        {
            if (genre == null || genre.Name.Length == 0 || genre.Id<=0)
                throw new ArgumentException("Wrong game model");

            await _genreRepository.UpdateAsync(ToDalEntity(genre));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGenre> GetAsync(int id)
        {
            Genre genre = await _genreRepository.SelectByIdAsync(id);
            return ToBlEntity(genre);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByGenreAsync(int id)
        {
            Genre genre = await _genreRepository.SelectByIdAsync(id,x=>x.GameGenres);
       
            return ToBlEntity(genre).Games;
        }

        public async Task<IEnumerable<BLGenre>> GetAllAsync()
        {
            return ToBlEntity(
                await _genreRepository.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await _genreRepository.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }
    }

}
