using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    public class CommentsControllerTest
    {
        private Mock<ICommentService> _commentService;
        private CommentsController _commentsController;

        [SetUp]
        public void Setup()
        {
            _commentService = new Mock<ICommentService>();
            _commentsController = new CommentsController(_commentService.Object);
        }

        [Test]
        public async Task Get_IdOfGame_ReturnsListOfComments()
        {
            //arrange
            IEnumerable<BLComment> comments = new List<BLComment> { new BLComment() { Body = "Body" } };
            _commentService.Setup(cs => cs.GetCommentsForPostAsync(1)).Returns(Task.FromResult(comments));
            //act
            var result = await _commentsController.Get(1) as OkObjectResult;
            //assert
            _commentService.Verify(cs => cs.GetCommentsForPostAsync(1));
            Assert.NotNull(result);
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(result.Value,comments);
        }
        [Test]
        public async Task Get_WrongIdOfGame_ReturnsNotFound()
        {
            //arrange
            _commentService.Setup(cs => cs.GetCommentsForPostAsync(-1)).ReturnsAsync((IEnumerable<BLComment>) null);
            //act
            var result = await _commentsController.Get(-1);
            //assert
            _commentService.Verify(cs => cs.GetCommentsForPostAsync(-1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
        [Test]
        public async Task Post_IdOfGameAndNewComment_Returns201()
        {
            //arrange
            BLComment comment = new BLComment { Body = "Body" ,Name = "Name",GameId = 1};
            _commentService.Setup(cs => cs.AddAsync(comment)).Returns(Task.FromResult(true)).Verifiable();
            //act
            var result = await _commentsController.Post(1,comment);
            //assert
            _commentService.Verify(cs => cs.AddAsync(comment));
            Assert.AreEqual(result.GetType(), typeof(CreatedResult));
        }
        [Test]
        public async Task Post_IncorrectValues_Returns404()
        {
            //arrange
            BLComment commentDal = null;
            BLComment comment = new BLComment { Body = "Body", Name = "Name", GameId = 1 };
            _commentService.Setup(cs => cs.GetById(1)).Returns(Task.FromResult(commentDal));
            //act
            var result = await _commentsController.AnswerToComment(2, 1, comment);
            //assert
            _commentService.Verify(cs => cs.GetById(1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
        [Test]
        public async Task AnswerToComment_CorrectValues_Returns200()
        {
            //arrange
            BLComment comment = new BLComment { Body = "Body", Name = "Name", GameId = 1 };
            _commentService.Setup(cs => cs.AddAsync(comment)).Returns(Task.FromResult(true)).Verifiable();
            _commentService.Setup(cs => cs.GetById(1)).Returns(Task.FromResult(comment));
            //act
            var result = await _commentsController.AnswerToComment(2, 1, comment);
            //assert
            _commentService.Verify(cs => cs.AddAsync(comment));
            _commentService.Verify(cs => cs.GetById(1));
            Assert.AreEqual(result.GetType(), typeof(CreatedResult));
        }

        [Test]
        public async Task AnswerToComment_IncorrectValues_Returns404()
        {
            //arrange
            BLComment commentDal = null;
            BLComment comment = new BLComment { Body = "Body", Name = "Name", GameId = 1 };
            _commentService.Setup(cs => cs.GetById(1)).Returns(Task.FromResult(commentDal));
            //act
            var result = await _commentsController.AnswerToComment(2, 1, comment);
            //assert
            _commentService.Verify(cs => cs.GetById(1));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public async Task Delete_CorrectValues_Returns200()
        {
            //arrange
            BLComment comment = new BLComment { Body = "Body", Name = "Name", GameId = 1 };
            _commentService.Setup(cs => cs.DeleteAsync(1)).Returns(Task.FromResult(true)).Verifiable();
            _commentService.Setup(cs => cs.GetById(1)).Returns(Task.FromResult(comment));
            //act
            var result = await _commentsController.Delete(1);
            //assert
            _commentService.Verify(cs => cs.DeleteAsync(1));
            _commentService.Verify(cs => cs.GetById(1));
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }

        [Test]
        public async Task Delete_IncorrectValues_Returns404()
        {
            //arrange
            BLComment comment = null;
            _commentService.Setup(cs => cs.GetById(111)).Returns(Task.FromResult(comment));
            //act
            var result = await _commentsController.Delete(111);
            //assert
            _commentService.Verify(cs => cs.GetById(111));
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
    }
}