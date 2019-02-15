using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class PublisherRepository: GenericRepository<Publisher>,IPublisherRepository
    {
        public PublisherRepository(IDataContext db) : base(db)
        {

        }
        public async Task<Publisher> GetPublisherWithGamesAsync(int id)
        {
            return await Db.Set<Publisher>().Include(x => x.Games).FirstAsync(x => x.Id == id);
        }
    }
}
