using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T :class
    {
        protected readonly IDataContext Db;

        protected GenericRepository(IDataContext db)
        {
            Db = db;
        }

        public virtual async Task DeleteAsync(int? id)
        {
            T deleted = await SelectByIdAsync(id);

            if (deleted != null)

                Db.Set<T>().Remove(deleted);
        }



        public virtual async Task<IEnumerable<T>> SelectAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }



        public virtual async Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Db.Set<T>().Where(predicate).ToListAsync();
        }



        public virtual async Task<T> SelectByIdAsync(int? id)
        {
            return await Db.Set<T>().FindAsync(id);
        }



        public virtual async Task InsertAsync(T user)
        {
              await Db.Set<T>().AddAsync(user);
        }



        public virtual async Task UpdateAsync(T user)
        {
            await Task.Run(() => Db.Entry(user).State = EntityState.Modified);
        }

    }
}
