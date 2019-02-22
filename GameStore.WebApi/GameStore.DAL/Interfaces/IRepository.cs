using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> SelectAllAsync();

        Task<T> SelectByIdAsync(int? id);

        Task InsertAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(int? id);

        //Task SaveAsync();

        Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>> predicate);
    }
}
