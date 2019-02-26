using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models
{
    public class GamesFiltersModel
    {
        public List<string> Genres { get; set; }
        public List<string> Platforms { get; set; }
        public List<string> Publishers { get; set; }

        public SortByType SortBy { get; private set; }

        public int PriceFrom { get; private set; }
        public int PriceTo { get; private set; }

        public string PartOfName { get; private set; }

        public GamesFiltersModel()
        {
            Genres = new List<string>();
            Platforms = new List<string>();
            Publishers = new List<string>();
            SortBy = SortByType.Newest;
        }

        public void SetSortingBy(string type)
        {
            if (!Enum.TryParse(type, true, out SortByType result))
                return;

            SortBy = result;
        }

        public void SetPriceRange(int from, int to)
        {
            if (from >= 0)
                PriceFrom = from;
            if (to >= 0 && to > from)
                PriceTo = to;
        }

        public void FindByNamePart(string name)
        {
            if (name != null && name.Length > 2)
                PartOfName = name;
        }
    }

    public enum SortByType
    {
        MostCommented,
        PriceAsc,
        PriceDesc,
        Newest,
    }
}
