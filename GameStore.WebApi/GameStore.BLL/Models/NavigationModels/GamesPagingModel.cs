using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models.NavigationModels
{
    public class GamesPagingModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int ToSkip { get; private set; }
        public int ToTake { get; private set; }


        public GamesPagingModel(int amountOfItems,int pageNumber, int showedItemsCurrency)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(amountOfItems / (double)showedItemsCurrency);    
            ToSkip = pageNumber*showedItemsCurrency-showedItemsCurrency;
            ToTake = showedItemsCurrency;
        }
    }
}
