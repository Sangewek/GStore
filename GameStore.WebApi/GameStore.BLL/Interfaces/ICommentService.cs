using GameStore.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService:IService
    {
        Task Add(BLComment comment);
        Task<IEnumerable<BLComment>> GetCommentsForPost(int id);
        Task Delete(int id);
    }
}
