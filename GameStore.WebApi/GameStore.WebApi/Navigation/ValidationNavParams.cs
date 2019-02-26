using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.BLL.Models.NavigationModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Navigation
{
    public class ValidationNavParams
    {
        public static GamesNavigationModel ValidateNavigationBind(GamesNavBind bindModel)
        {
            GamesFiltersModel filters = new GamesFiltersModel();
            GamesPagingModel pages = new GamesPagingModel();

            if (bindModel.Genres != null && bindModel.Genres.Length > 0)
                foreach (var genre in bindModel.Genres)
                    if(genre!=null)
                    filters.Genres.Add(genre);

            if (bindModel.Platforms != null && bindModel.Platforms.Length > 0)
                foreach (var platform in bindModel.Platforms)
                    if (platform != null)
                        filters.Platforms.Add(platform);

            if (bindModel.Publishers != null && bindModel.Publishers.Length > 0)
                foreach (var publisher in bindModel.Publishers)
                    if (publisher != null)
                        filters.Publishers.Add(publisher);

            filters.SetSortingBy(bindModel.SortBy);
            filters.SetPriceRange(bindModel.PriceFrom, bindModel.PriceTo);

            if (bindModel.PartOfName != null && bindModel.PartOfName.Length>2)
                filters.FindByNamePart(bindModel.PartOfName);

            pages.ToTake = bindModel.AmountToShow>0 ? bindModel.AmountToShow : 5;

            pages.PageNumber = bindModel.PageNumber > 0 ? bindModel.PageNumber : 1;

            GamesNavigationModel navigationModel = new GamesNavigationModel { PagesInfo = pages, Filters = filters };
            return navigationModel;
        }
    }
}
