using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebApi.Navigation
{
    public class GamesNavBind
    {
        public string[] Genres { get; set; }
        public string[] Platforms { get; set; }
        public string[] Publishers { get; set; }
        public string SortBy { get; set; }

        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public string PartOfName { get; set; }
        public int PageNumber { get; set; }
        public int AmountToShow { get; set; }
    }
}
