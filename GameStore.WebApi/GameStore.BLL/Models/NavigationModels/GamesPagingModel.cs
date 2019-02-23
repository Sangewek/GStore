using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models.NavigationModels
{
    public class GamesPagingModel
    {
        private int _pageNumber;
        private int _itemsAmount;
        private int _toTake;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value > 0)
                    _pageNumber = value;
            }
        }

        public int TotalPages
        {
            get
            {
                if (_itemsAmount > 0 && _toTake > 0)
                    return (int)Math.Ceiling(_itemsAmount / (double)_toTake);
                else return 0;
            }
        }

        public int ItemsAmount
        {
            get
            {
                return _itemsAmount;
            }
            set
            {
                if (value > 0)
                    _itemsAmount = value;
            }
        }

        public int ToSkip
        {
            get
            {
                if (_toTake > 0 && _pageNumber > 1 && _toTake * _pageNumber - _toTake > 0)
                    return _toTake * _pageNumber - _toTake;
                else return 0;
            }
        }

        public int ToTake
        {
            get
            {
                return _toTake;
            }
            set
            {
                if (value > 0)
                    _toTake = value;
            }
        }
    }
}
