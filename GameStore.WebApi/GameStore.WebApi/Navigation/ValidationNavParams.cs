using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.BLL.Models.NavigationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameStore.WebApi.Navigation
{
    public class ValidationNavParams
    {
        public static GamesNavigationModel ValidateNavigationBind(GamesNavBind bindModel, ModelStateDictionary modelState)
        {
            GamesFiltersModel filters = new GamesFiltersModel();
            GamesPagingModel pages = new GamesPagingModel();

            if (bindModel.Genres != null && bindModel.Genres.Length > 0)
                foreach (var genre in bindModel.Genres)
                    if (genre != null)
                        filters.Genres.Add(genre);
                    else modelState.AddModelError("Genre", "One of genre models was nullable");

            if (bindModel.Platforms != null && bindModel.Platforms.Length > 0)
                foreach (var platform in bindModel.Platforms)
                    if (platform != null)
                        filters.Platforms.Add(platform);
                    else modelState.AddModelError("Platform", "One of platform models was nullable");


            if (bindModel.Publishers != null && bindModel.Publishers.Length > 0)
                foreach (var publisher in bindModel.Publishers)
                    if (publisher != null)
                        filters.Publishers.Add(publisher);
                    else modelState.AddModelError("Publisher", "One of publisher models was nullable");

            filters.SetSortingBy(bindModel.SortBy);
            if (filters.SortBy.ToString() == "Newest" && bindModel.SortBy != "Newest")
            {
                modelState.AddModelError("SortBy", "Chose method of sorting was not found");
            }

            if (bindModel.PriceFrom < 0 || bindModel.PriceTo < bindModel.PriceFrom)
            {
                modelState.AddModelError("Price", "Chose price range is incorrect");
            }
            else
            {
                filters.SetPriceRange(bindModel.PriceFrom, bindModel.PriceTo);
            }

            if (bindModel.PartOfName != null)
                if (bindModel.PartOfName.Length > 2)
                {
                    filters.FindByNamePart(bindModel.PartOfName);
                }
                else
                {
                    modelState.AddModelError("Search by name", "Passed part of name must be more than 2 symbols");
                }

            if (bindModel.AmountToShow > 0)
            {
                pages.ToTake = bindModel.AmountToShow;
            }
            else
            {
                modelState.AddModelError("Capacity of shown items", "Capacity of shown items must be more than 0");
            }

            if (bindModel.PageNumber > 0)
            {
                pages.PageNumber = bindModel.PageNumber;
            }
            else
            {
                modelState.AddModelError("Page number", "Number of the choose page must be more than 0");
            }

            GamesNavigationModel navigationModel = new GamesNavigationModel { PagesInfo = pages, Filters = filters };
            return navigationModel;
        }
    }
}
