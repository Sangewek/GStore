using System;
using System.Collections.Generic;
using System.Text;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;

namespace GameStore.DAL.Repositories
{
    public class PublisherRepository: GenericRepository<Publisher>,IPublisherRepository
    {
        public PublisherRepository(DataContext db) : base(db)
        {
        }
    }
}
