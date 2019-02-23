using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.WebApi.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfAddition { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public string Publisher { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<string> Genres { get; set; }
        public ICollection<string> Platforms { get; set; }
    }
}
