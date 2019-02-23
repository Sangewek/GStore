using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Models.NavigationModels;
using GameStore.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace GameStore.BLL.Services
{
    public class GameService : BaseService<BLGame, Game>, IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlatformRepository _platformRepository;

        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository,
            IPlatformRepository platformRepository) : base(unitOfWork)
        {
            _gameRepository = gameRepository;
            _platformRepository = platformRepository;
        }

        public async Task AddAsync(BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0)
                throw new ArgumentException("Wrong game model");

            game.DateOfAddition = DateTime.Now;
            ;
            await _gameRepository.InsertAsync(ToDalEntity(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BLGame game)
        {
            if (game == null || game.PublisherId <= 0 || game.Name.Length == 0 || game.Id <= 0)
                throw new ArgumentException("Wrong game model");

            await _gameRepository.UpdateAsync(ToDalEntity(game));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLGame> GetAsync(int id)
        {
            Game game = await _gameRepository.SelectByIdAsync(id);
            return ToBlEntity(game);
        }

        public async Task<IEnumerable<BLGenre>> GetGenresByGameAsync(int id)
        {
            Game game = await _gameRepository.SelectByIdAsync(id);

            return ToBlEntity(game).Genres;
        }

        public async Task<IEnumerable<BLGame>> GetGamesByPlatformAsync(int id)
        {
            Platform platform = await _platformRepository.SelectByIdAsync(id, x => x.GamePlatform);
            var games = BaseService<BLPlatform, Platform>.ToBlEntity(platform).Games;
            return games;
        }

        public async Task<IEnumerable<BLGame>> GetAllAsync()
        {
            return ToBlEntity(
                await _gameRepository.SelectAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            if (await _gameRepository.SelectByIdAsync(id) != null)
                await _gameRepository.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }

        public async Task<string> DownloadGame(int id)
        {
            return (await _gameRepository.SelectByIdAsync(id)).Name.Replace(" ", "_");
        }

        public IEnumerable<BLGame> NavigateByGames(GamesFiltersModel filters,
            GamesPagingModel gamesPaging)
        {
            if (filters == null || gamesPaging == null)
                throw new ArgumentException("Wrong filter or paging model");

            var filterExpression = NavigationGameService.GetFilterExpression(filters, gamesPaging);
            Expression<Func<Game, object>> sortByExpression = NavigationGameService.GetExpressionForSorting(filters.SortBy);

            List<Game> games = (_gameRepository.GetGamesByNavigation(filterExpression, sortByExpression,
                gamesPaging.ToSkip, gamesPaging.ToTake)).ToList();

            if (filters.SortBy == SortByType.PriceDesc || filters.SortBy == SortByType.MostCommented)
                games.Reverse();

            return ToBlEntity(games);
        }
    }
}
