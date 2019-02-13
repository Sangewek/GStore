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
    public class CommentService: GenericService<Comment>,ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(IUnitOfWork unitOfWork, ICommentRepository repository) : base(unitOfWork)
        {
            this._repository = repository;
        }

        public async Task Add(BLComment comment)
        {
             await _repository.InsertAsync(AutoMapper.Mapper.Map<BLComment, Comment>(comment));
        }

        public async Task<IEnumerable<BLComment>> GetCommentsForPost(int id)
        {
           return AutoMapper.Mapper.Map<IEnumerable<Comment>,IEnumerable<BLComment>>(
               await _repository.SelectAllAsync(x=>x.Game.Id==id));
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
           }

    }
}
