using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models.NavigationModels
{
    public class GamesNavigationModel
    {
        public IEnumerable<BLGame> SelectedGames { get; set; }
        public GamesFiltersModel Filters { get; set; }
        public GamesPagingModel PagesInfo { get; set; }
    }
}
