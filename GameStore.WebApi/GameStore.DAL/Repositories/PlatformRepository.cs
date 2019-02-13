using System;
using System.Collections.Generic;
using System.Text;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;

namespace GameStore.DAL.Repositories
{
    public class PlatformRepository: GenericRepository<Platform>,IPlatformRepository
    {
        public PlatformRepository(DataContext db) : base(db)
        {
        }
    }
}
