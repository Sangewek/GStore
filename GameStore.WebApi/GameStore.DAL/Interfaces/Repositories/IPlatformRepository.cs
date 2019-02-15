using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Models;

namespace GameStore.DAL.Interfaces.Repositories
{
    public interface IPlatformRepository: IRepository<Platform>
    {
         Task<Platform> GetPlatformWithGames(int id);

    }
}
