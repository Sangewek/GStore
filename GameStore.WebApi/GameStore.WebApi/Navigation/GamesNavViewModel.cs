using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.BLL.Models.NavigationModels;
using GameStore.WebApi.ViewModels;

namespace GameStore.WebApi.Navigation
{
    public class GamesNavViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }
        public GamesPagingModel Pages { get; set; }
        public GamesFiltersModel Filters { get; set; }
    }
}
