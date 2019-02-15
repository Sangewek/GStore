using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Models;

namespace GameStore.DAL.Interfaces.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> GetPublisherWithGamesAsync(int id);
    }
}
