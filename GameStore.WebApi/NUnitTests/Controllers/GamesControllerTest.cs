using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    public class GamesControllerTest
    {
        private Mock<IGameService> _gameService;
        private GamesController _gamesController;

        [SetUp]
        public void Setup()
        {
            _gameService = new Mock<IGameService>();
            _gamesController = new GamesController(_gameService.Object);
        }

        [Test]
        public async Task GetAll_Nothing_ReturnsListOfGames()
        {
            //arrange
            IEnumerable<BLGame> games = new List<BLGame> { new BLGame() { Name = "Name",Description = "Description",PublisherId = 1} };
            _gameService.Setup(cs => cs.GetAllAsync()).Returns(Task.FromResult(games));
            //act
            var result = await (_gamesController).GetAll() as OkObjectResult;
            //assert
            _gameService.Verify(cs => cs.GetAllAsync());
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(result.Value, games);
        }
        [Test]
        public async Task GetAll_Nothing_NotFound()
        {
            //arrange
            IEnumerable<BLGame> games =new List<BLGame>();
            _gameService.Setup(cs => cs.GetAllAsync()).Returns(Task.FromResult(games));
            //act
            var result = await (_gamesController).GetAll() as NotFoundResult;
            //assert
            _gameService.Verify(cs => cs.GetAllAsync());
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));


        }
        [Test]
        public async Task Get_CorrectId_ReturnGame200()
        {
            //arrange
            BLGame game = new BLGame  { Id=1,Name = "Name", Description = "Description", PublisherId = 1  };
            _gameService.Setup(cs => cs.GetAsync(1)).Returns(Task.FromResult(game));
            //act
            var result = await (_gamesController).Get(1) as OkObjectResult;
            //assert
            _gameService.Verify(cs => cs.GetAsync(1));
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(result.Value, game);
        }
        [Test]
        public async Task Get_IncorrectId_ReturnNotFound404()
        {
            //arrange
            BLGame game = null;
            _gameService.Setup(cs => cs.GetAsync(1)).Returns(Task.FromResult(game));
            //act
            var result = await (_gamesController).Get(1) as NotFoundResult;
            //assert
            _gameService.Verify(cs => cs.GetAsync(1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public async Task Post_CorrectGame_AddedGame200()
        {
            //arrange
            BLGame game = new BLGame { Name = "Name", Description = "Description", PublisherId = 1 };
            _gameService.Setup(cs => cs.AddAsync(game)).Returns(Task.FromResult(true)).Verifiable();
            //act
            var result = await _gamesController.Post(game) as CreatedResult;
            //assert
            _gameService.Verify(cs => cs.AddAsync(game));
            Assert.AreEqual(result.GetType(), typeof(CreatedResult));
        }

        [Test]
        public async Task Post_IncorrectGame_BadRequest()
        {
            //arrange
            //act
            var result = await _gamesController.Post(null);
            //assert
            Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));
        }

        [Test]
        public async Task Put_CorrectGame_UpdatedGame200()
        {
            //arrange
            BLGame game = new BLGame { Name = "Name", Description = "Description", PublisherId = 1 };
            _gameService.Setup(cs => cs.UpdateAsync(game)).Returns(Task.FromResult(true)).Verifiable();
            //act
            var result = await _gamesController.Put(1,game);
            //assert
            _gameService.Verify(cs => cs.UpdateAsync(game));
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }

        [Test]
        public async Task Put_IncorrectGame_BadRequest()
        {
            //arrange
            BLGame game = new BLGame { Name = "", Description = "Description", PublisherId = 1 };
            //act
            var result = await _gamesController.Post(game);
            //assert
            Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));
        }
        [Test]
        public async Task Delete_CorrectValues_Returns200()
        {
            //arrange
            BLGame game = new BLGame { Name = "Name", Description = "Description", PublisherId = 1 };
            _gameService.Setup(cs => cs.DeleteAsync(1)).Returns(Task.FromResult(true)).Verifiable();
            _gameService.Setup(cs => cs.GetAsync(1)).Returns(Task.FromResult(game));
            //act
            var result = await _gamesController.Delete(1);
            //assert
            _gameService.Verify(cs => cs.DeleteAsync(1));
            _gameService.Verify(cs => cs.GetAsync(1));
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }

        [Test]
        public async Task Delete_IncorrectValues_Returns404()
        {
            //arrange
            BLGame game = null;
            _gameService.Setup(cs => cs.GetAsync(111)).Returns(Task.FromResult(game));
            //act
            var result = await _gamesController.Delete(111);
            //assert
            _gameService.Verify(cs => cs.GetAsync(111));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
        [Test]
        public async Task GetByPlatform_CorrectId_ReturnGames200()
        {
            //arrange
            IEnumerable<BLGame> games = new List<BLGame> { new BLGame() { Name = "Name", Description = "Description", PublisherId = 1 } };
            _gameService.Setup(cs => cs.GetGamesByPlatformAsync(1)).Returns(Task.FromResult(games));
            //act
            var result = await (_gamesController).GetByPlatform(1) as OkObjectResult;
            //assert
            _gameService.Verify(cs => cs.GetGamesByPlatformAsync(1));
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(result.Value, games);
        }
        [Test]
        public async Task GetByPlatform_IncorrectId_ReturnNotFound404()
        {
            //arrange
            IEnumerable<BLGame> games = new List<BLGame>();
            _gameService.Setup(cs => cs.GetGamesByPlatformAsync(1)).Returns(Task.FromResult(games));
            //act
            var result = await (_gamesController).GetByPlatform(1) as NotFoundResult;
            //assert
            _gameService.Verify(cs => cs.GetGamesByPlatformAsync(1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public async Task GetGenres_CorrectId_ReturnGames200()
        {
            //arrange
            IEnumerable<BLGenre> genres = new List<BLGenre> { new BLGenre() { Name = "Name"} };
            _gameService.Setup(cs => cs.GetGenresByGameAsync(1)).Returns(Task.FromResult(genres));
            //act
            var result = await (_gamesController).GetGenres(1) as OkObjectResult;
            //assert
            _gameService.Verify(cs => cs.GetGenresByGameAsync(1));
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(result.Value, genres);
        }
        [Test]
        public async Task GetGenres_IncorrectId_ReturnNotFound404()
        {
            //arrange
            IEnumerable<BLGenre> genres = new List<BLGenre>();
            _gameService.Setup(cs => cs.GetGenresByGameAsync(1)).Returns(Task.FromResult(genres));
            //act
            var result = await (_gamesController).GetGenres(1) as NotFoundResult;
            //assert
            _gameService.Verify(cs => cs.GetGenresByGameAsync(1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
    }
}
