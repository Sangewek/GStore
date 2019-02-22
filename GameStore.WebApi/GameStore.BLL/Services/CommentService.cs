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
    public class CommentService: BaseService,ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(BLComment comment)
        {
             await UnitOfWork.Comments.InsertAsync(AutoMapper.Mapper.Map<BLComment, Comment>(comment));
             await UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BLComment>> GetCommentsForPostAsync(int id)
        {
            IEnumerable<Comment> comments = await UnitOfWork.Comments.SelectAllAsync(x => x.GameId == id);
           return AutoMapper.Mapper.Map<IEnumerable<Comment>,IEnumerable<BLComment>>(comments              );
        }

        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Comments.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
           }

    }
}
