using GameStore.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ICommentRepository Comments { get; }
        IGameRepository Games { get; }
        IGenreRepository Genres { get; }
        IPlatformRepository Platforms { get; }
        IPublisherRepository Publishers { get; }

        Task SaveAsync();
    }
}
