using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService:IDisposable
    {
        Task AddAsync(BLComment comment);
        Task<IEnumerable<BLComment>> GetCommentsForPostAsync(int id);
        Task DeleteAsync(int id);
        Task<BLComment> GetById(int id);
    }
}
