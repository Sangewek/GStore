﻿using System;
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
    public class GameRepository : GenericRepository<Game>,IGameRepository
    {
        public GameRepository(IDataContext db) : base(db)
        {
        }

        public async Task<Game> GetGameGenres(int id)
        {
            return await Db.Set<Game>().Include(x => x.GameGenres).FirstAsync(x => x.Id == id);
        }

    }
}