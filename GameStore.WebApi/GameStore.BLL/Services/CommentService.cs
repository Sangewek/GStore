using GameStore.BLL.Models;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class CommentService : BaseService<BLComment,Comment>, ICommentService
    {
        private readonly ICommentRepository _commentService;
        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentService) : base(unitOfWork)
        {
            this._commentService = commentService;
        }

        public async Task AddAsync(BLComment comment)
        {
            if(comment == null || comment.Body.Length==0 || comment.GameId<0 || comment.Name.Length==0)
                throw new ArgumentException("Wrong comment model");

            await _commentService.InsertAsync(ToDalEntity(comment));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLComment> GetById(int id)
        {
            return ToBlEntity(await _commentService.SelectByIdAsync(id));
        }

        public async Task<IEnumerable<BLComment>> GetCommentsForPostAsync(int id)
        {
            IEnumerable<Comment> comments = await _commentService.SelectAllAsync(x => x.GameId == id);
            return ToBlEntity(comments);
        }

        public async Task DeleteAsync(int id)
        {
            Comment comment = await _commentService.SelectByIdAsync(id);
            if (comment?.ParentCommentId != null)
            {
                comment.ParentCommentId = null;
                await _commentService.UpdateAsync(comment);
            }

            if (comment != null)
            {
                await _commentService.DeleteAsync(id);
                await UnitOfWork.SaveAsync();
            }
        }

    }
}
