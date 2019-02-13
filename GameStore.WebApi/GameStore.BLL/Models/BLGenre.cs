
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models
{
    public class BLGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BLGenre BaseGenre { get; set; }

        public ICollection<BLGame> Games { get; set; }
        public ICollection<BLGenre> SubGenres { get; set; }
    }
}
