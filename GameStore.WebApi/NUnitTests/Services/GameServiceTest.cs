using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Mapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Services
{
    public class GameServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private GameService _gameService;

        static GameServiceTest()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfile<MapToBLModels>();
            });
        }

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _gameService = new GameService(_unitOfWork.Object);


        }
        [Test]
        public async Task AddAsync_CorrectGame_AddToDB()
        {
            //arrange
            BLGame game = new BLGame() { Description = "Description", PublisherId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Games.InsertAsync(It.IsAny<Game>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            await _gameService.AddAsync(game);
            //assert
            _unitOfWork.Verify(cs => cs.Games.InsertAsync(It.IsAny<Game>()));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }

        [Test]
        public async Task AddAsync_IncorrectGame_ThrowsEx()
        {
            //arrange
            BLGame Game = new BLGame() { Description = "Description", PublisherId = 1, Name = "" };
            _unitOfWork.Setup(uow => uow.Games.InsertAsync(It.IsAny<Game>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            //assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _gameService.AddAsync(Game));
        }
        [Test]
        public async Task UpdateAsync_CorrectGame_AddToDB()
        {
            //arrange
            BLGame game = new BLGame() { Id = 1, Description = "Description", PublisherId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Games.UpdateAsync(It.IsAny<Game>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            await _gameService.UpdateAsync(game);
            //assert
            _unitOfWork.Verify(cs => cs.Games.UpdateAsync(It.IsAny<Game>()));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }

        [Test]
        public async Task UpdateAsync_IncorrectGame_ThrowsEx()
        {
            //arrange
            BLGame game = new BLGame() { Id = 0, Description = "Description", PublisherId = 1, Name = "" };
            _unitOfWork.Setup(uow => uow.Games.UpdateAsync(It.IsAny<Game>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            //assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _gameService.AddAsync(game));
        }

        [Test]
        public async Task GetById_CorrectId_ReturnBLModel()
        {
            //arrange
            Game game = new Game() { Description = "Description", PublisherId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Games.SelectByIdAsync(1)).Returns(Task.FromResult(game)).Verifiable();
            //act
            var result = await _gameService.GetAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Games.SelectByIdAsync(1));
            Assert.AreEqual(result.Description, game.Description);
        }
        [Test]
        public async Task GetAll_Nothing_ReturnBLModels()
        {
            //arrange
            IEnumerable<Game> games = new List<Game> { new Game { Description = "Description", PublisherId = 1, Name = "Name" } };

            _unitOfWork.Setup(uow => uow.Games.SelectAllAsync()).Returns(Task.FromResult(games)).Verifiable();
            //act
            var result = await _gameService.GetAllAsync();
            //assert
            _unitOfWork.Verify(cs => cs.Games.SelectAllAsync());
            Assert.AreEqual(result.First().Description, games.First().Description);
        }

        [Test]
        public async Task DeleteAsync_CorrectId_DeletedModelInDB()
        {
            //arrange
            Game game = new Game() { Description = "Description", PublisherId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Games.SelectByIdAsync(1)).Returns(Task.FromResult(game)).Verifiable();
            _unitOfWork.Setup(uow => uow.Games.DeleteAsync(1)).Returns(Task.FromResult(0)).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.FromResult(0)).Verifiable();

            //act
            await _gameService.DeleteAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Games.SelectByIdAsync(1));
            _unitOfWork.Verify(cs => cs.Games.DeleteAsync(1));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }

        [Test]
        public async Task DeleteAsync_IncorrectId_Nothing()
        {
            //arrange
            Game game = null;
            _unitOfWork.Setup(uow => uow.Games.SelectByIdAsync(1)).Returns(Task.FromResult(game)).Verifiable();

            //act
            await _gameService.DeleteAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Games.SelectByIdAsync(1));
        }
    }
}
