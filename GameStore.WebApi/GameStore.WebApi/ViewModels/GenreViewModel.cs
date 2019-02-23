using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebApi.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseGenreId { get; set; }

        public ICollection<string> Games { get; set; }
        public ICollection<string> SubGenres { get; set; }
    }
}
