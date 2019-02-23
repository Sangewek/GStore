using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GameStore.BLL.Models;
using GameStore.BLL.Models.NavigationModels;
using GameStore.DAL.Models;

namespace GameStore.BLL.Services
{
    public class NavigationGameService
    {
        public static Expression<Func<Game,bool>> GetFilterExpression(GamesFiltersModel filters, GamesPagingModel gamesPaging)
        {
            var param = Expression.Parameter(typeof(Game), "x");
            BinaryExpression bodyExpression = null;

            if (filters.Genres?.Count > 0)
                bodyExpression = GetExpressionForGenres(filters.Genres, bodyExpression, param);

            if (filters.Platforms?.Count > 0)
                bodyExpression = GetExpressionForPlatforms(filters.Platforms, bodyExpression, param);

            if (filters.Publishers?.Count > 0)
                bodyExpression = GetExpressionForPublisher(filters.Publishers, bodyExpression, param);

            if (filters.PartOfName != null)
                bodyExpression = GetExpressionForNameFilter(filters.PartOfName, bodyExpression, param);

            bodyExpression = GetExpressionForPriceFilter(filters.PriceFrom, filters.PriceTo, bodyExpression, param);

            return Expression.Lambda<Func<Game, bool>>(bodyExpression, param);
        }

        public static Expression<Func<Game, object>> GetExpressionForSorting(SortByType sortType)
        {
            Expression<Func<Game, object>> predicate = null;
            if (sortType == SortByType.Newest)
                predicate = x => x.DateOfAddition;
            if (sortType == SortByType.MostCommented)
                predicate = x => x.Comments.Count();
            if (sortType == SortByType.PriceAsc || sortType == SortByType.PriceDesc)
                predicate = x => x.Price;

            return predicate;
        }

        private static BinaryExpression GetExpressionForGenres(List<string> filterList, BinaryExpression expression, ParameterExpression param)
        {
            foreach (var genre in filterList)
            {
                Expression<Func<Game, bool>> predicate = x => x.GameGenres.Any(y => y.Genre.Name == genre);
                expression = SetExpression(expression, predicate, param);
            }
            return expression;
        }

        private static BinaryExpression GetExpressionForPlatforms(List<string> filterList, BinaryExpression expression, ParameterExpression param)
        {
            foreach (var platform in filterList)
            {
                Expression<Func<Game, bool>> predicate = x => x.GamePlatform.Any(y => y.Platform.Name == platform);
                expression = SetExpression(expression, predicate, param);
            }
            return expression;
        }

        private static BinaryExpression GetExpressionForPublisher(List<string> filterList, BinaryExpression expression, ParameterExpression param)
        {
            foreach (var publisher in filterList)
            {
                Expression<Func<Game, bool>> predicate = x => x.Publisher.Name == publisher;
                expression = SetExpression(expression, predicate, param);
            }
            return expression;
        }

        private static BinaryExpression GetExpressionForNameFilter(string namePart, BinaryExpression expression, ParameterExpression param)
        {
            Expression<Func<Game, bool>> predicate = x => x.Name.Contains(namePart);
            expression = SetExpression(expression, predicate, param);
            return expression;
        }

        private static BinaryExpression GetExpressionForPriceFilter(int priceFrom, int priceTo, BinaryExpression expression, ParameterExpression param)
        {
            Expression<Func<Game, bool>> predicate = x => x.Price > priceFrom;
            expression = SetExpression(expression, predicate, param);
            if (priceTo > priceFrom)
            {
                predicate = x => x.Price < priceTo;
                expression = SetExpression(expression, predicate, param);
            }
            return expression;
        }

        private static BinaryExpression SetExpression(BinaryExpression expression, Expression<Func<Game, bool>> predicate, ParameterExpression param)
        {
            if (expression == null)
            {
                Expression<Func<Game, bool>> initial = x => x != null;
                expression = Expression.AndAlso(
                    Expression.Invoke(initial, param),
                    Expression.Invoke(predicate, param));
            }
            else
            {
                expression = Expression.AndAlso(
                    expression,
                    Expression.Invoke(predicate, param));
            }
            return expression;
        }
    }
}
