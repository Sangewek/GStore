using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Models;

namespace GameStore.DAL.Interfaces.Repositories
{
    public interface IGameRepository:IRepository<Game>
    {
        IEnumerable<Game> GetGamesByNavigation(Expression<Func<Game, bool>> filter,
            Expression<Func<Game,object>> orderBy,
            int skip, int take);
    }
}
