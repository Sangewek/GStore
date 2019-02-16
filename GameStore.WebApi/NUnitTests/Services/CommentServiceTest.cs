using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Mapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;
using GameStore.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Services
{
    public class CommentServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private CommentService _commentService;

         static CommentServiceTest()
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
            _commentService = new CommentService(_unitOfWork.Object);
    
        }

        [Test]
        public async Task AddAsync_CorrectComment_AddToDB()
        {
            //arrange
            BLComment comment = new BLComment() { Body = "Body", GameId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Comments.InsertAsync(It.IsAny<Comment>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            await _commentService.AddAsync(comment);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.InsertAsync(It.IsAny<Comment>()));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }

        [Test]
        public async Task AddAsync_IncorrectComment_ThrowsEx()
        {
            //arrange
            BLComment comment = new BLComment() { Body = "Body", GameId = 1, Name = "" };
            _unitOfWork.Setup(uow => uow.Comments.InsertAsync(It.IsAny<Comment>())).Returns(Task.CompletedTask).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
            //act
            //assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _commentService.AddAsync(comment));
        }


        [Test]
        public async Task GetById_CorrectId_ReturnBLModel()
        {
            //arrange
            Comment comment = new Comment() { Body = "Body", GameId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Comments.SelectByIdAsync(1)).Returns(Task.FromResult(comment)).Verifiable();
            //act
            var result = await _commentService.GetById(1);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.SelectByIdAsync(1));
            Assert.AreEqual(result.Body, comment.Body);
        }


        [Test]
        public async Task GetCommentsForPostAsync_CorrectId_ReturnBLModel()
        {
            //arrange
            IEnumerable<Comment> comments = new List<Comment> { new Comment() { Body = "Body", GameId = 1, Name = "Name" } };
            _unitOfWork.Setup(uow => uow.Comments.SelectAllAsync(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(comments)).Verifiable();
            //act
            var result = await _commentService.GetCommentsForPostAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.SelectAllAsync(It.IsAny<Expression<Func<Comment, bool>>>()));
            Assert.AreEqual(result.First().Body, comments.First().Body);
        }

        [Test]
        public async Task DeleteAsync_CorrectId_DeletedModelInDB()
        {
            //arrange
            Comment comment = new Comment() { Body = "Body", GameId = 1, Name = "Name" };
            _unitOfWork.Setup(uow => uow.Comments.SelectByIdAsync(1)).Returns(Task.FromResult(comment)).Verifiable();
            _unitOfWork.Setup(uow => uow.Comments.DeleteAsync(1)).Returns(Task.FromResult(0)).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.FromResult(0)).Verifiable();

            //act
            await _commentService.DeleteAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.SelectByIdAsync(1));
            _unitOfWork.Verify(cs => cs.Comments.DeleteAsync(1));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }
        [Test]
        public async Task DeleteAsync_CorrectIdAndCommentWithParent_DeletedModelInDB()
        {
            //arrange
            Comment comment = new Comment() { Body = "Body", GameId = 1, Name = "Name", ParentCommentId = 2 };
            _unitOfWork.Setup(uow => uow.Comments.SelectByIdAsync(1)).Returns(Task.FromResult(comment)).Verifiable();
            _unitOfWork.Setup(uow => uow.Comments.DeleteAsync(1)).Returns(Task.FromResult(0)).Verifiable();
            _unitOfWork.Setup(uow => uow.Comments.UpdateAsync(It.IsAny<Comment>())).Returns(Task.FromResult(0)).Verifiable();
            _unitOfWork.Setup(uow => uow.SaveAsync()).Returns(Task.FromResult(0)).Verifiable();

            //act
            await _commentService.DeleteAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.SelectByIdAsync(1));
            _unitOfWork.Verify(cs => cs.Comments.DeleteAsync(1));
            _unitOfWork.Verify(uow => uow.Comments.UpdateAsync(It.IsAny<Comment>()));
            _unitOfWork.Verify(cs => cs.SaveAsync());
        }
        [Test]
        public async Task DeleteAsync_IncorrectId_Nothing()
        {
            //arrange
            Comment comment = null;
            _unitOfWork.Setup(uow => uow.Comments.SelectByIdAsync(1)).Returns(Task.FromResult(comment)).Verifiable();

            //act
            await _commentService.DeleteAsync(1);
            //assert
            _unitOfWork.Verify(cs => cs.Comments.SelectByIdAsync(1));
        }



    }
}
