using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class UnitOfWork: IDisposable, IUnitOfWork
    {
        private readonly IDataContext _dbContext;
        private bool _isDisposed;

        public UnitOfWork(IDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }
            _isDisposed = true;
        }

    }
}
