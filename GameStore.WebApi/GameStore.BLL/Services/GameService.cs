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
    public class GameService : GenericService<Game>,IGameService
    {
        private readonly IGameRepository _repository;
        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository) : base( unitOfWork)
        {
            this._repository = gameRepository;
        }

        public async Task Add(BLGame game)
        {
            await _repository.InsertAsync(AutoMapper.Mapper.Map<BLGame, Game>(game));
        }

        public async Task Update(BLGame game)
        {
            await _repository.UpdateAsync(AutoMapper.Mapper.Map<BLGame, Game>(game));
        }

        public async Task<BLGame> Get(int id)
        {
            Game game = await _repository.SelectByIdAsync(id);
            return AutoMapper.Mapper.Map<Game,BLGame>(game);
        }

        public async Task<IEnumerable<BLGame>> GetGamesByGenre(int id)
        {
           return AutoMapper.Mapper.Map<IEnumerable<Game>, IEnumerable<BLGame>>(await _repository.SelectAllAsync(x => x.GameGenres.Where(xx => xx.GenreId == id) != null));
        }

        public async Task<IEnumerable<BLGame>> GetGamesByPlatform(int id)
        {
            return AutoMapper.Mapper.Map<IEnumerable<Game>, IEnumerable<BLGame>>(await _repository.SelectAllAsync(x => x.GamePlatform.Where(xx => xx.PlatformId == id) != null));
        }

        public async Task<IEnumerable<BLGame>> GetAll()
        {
            return AutoMapper.Mapper.Map < IEnumerable <Game>,IEnumerable<BLGame>>(
                await _repository.SelectAllAsync());
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
