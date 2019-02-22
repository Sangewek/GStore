using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;

namespace GameStore.DAL.Repositories
{
    public class UnitOfWork: IDisposable, IUnitOfWork
    {
        private readonly IDataContext _dbContext;

        private  ICommentRepository _commentRepository;
        private  IGameRepository _gameRepository;
        private  IGenreRepository _genreRepository;
        private  IPlatformRepository _platformRepository;
        private  IPublisherRepository _publisherRepository;

        private bool _isDisposed;

        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_dbContext));
        public IGameRepository Games => _gameRepository ?? (_gameRepository = new GameRepository(_dbContext));
        public IGenreRepository Genres => _genreRepository ?? (_genreRepository = new GenreRepository(_dbContext));
        public IPlatformRepository Platforms => _platformRepository ?? (_platformRepository = new PlatformRepository(_dbContext));
        public IPublisherRepository Publishers => _publisherRepository ?? (_publisherRepository = new PublisherRepository(_dbContext));

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
