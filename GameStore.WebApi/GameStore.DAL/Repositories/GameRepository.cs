using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(IDataContext db) : base(db)
        {
        }

        public IEnumerable<Game> GetGamesByNavigation(Expression<Func<Game, bool>> filter, Expression<Func<Game, object>> orderBy,
            int skip, int take)
        {
            return Db.Set<Game>().Where(filter).Skip(skip).Take(take).OrderBy(orderBy);
        }
    }
}
