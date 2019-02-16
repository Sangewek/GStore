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
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLComment comment)
        {
            if(comment == null || comment.Body.Length==0 || comment.GameId<0 || comment.Name.Length==0)
                throw new ArgumentException("Wrong comment model");

            await UnitOfWork.Comments.InsertAsync(ToDalEntity(comment));
            await UnitOfWork.SaveAsync();
        }

        public async Task<BLComment> GetById(int id)
        {
            return ToBlEntity(await UnitOfWork.Comments.SelectByIdAsync(id));
        }

        public async Task<IEnumerable<BLComment>> GetCommentsForPostAsync(int id)
        {
            IEnumerable<Comment> comments = await UnitOfWork.Comments.SelectAllAsync(x => x.GameId == id);
            return ToBlEntity(comments);
        }

        public async Task DeleteAsync(int id)
        {
            Comment comment = await UnitOfWork.Comments.SelectByIdAsync(id);
            if (comment?.ParentCommentId != null)
            {
                comment.ParentCommentId = null;
                await UnitOfWork.Comments.UpdateAsync(comment);
            }

            if (comment != null)
            {
                await UnitOfWork.Comments.DeleteAsync(id);
                await UnitOfWork.SaveAsync();
            }
        }

    }
}
