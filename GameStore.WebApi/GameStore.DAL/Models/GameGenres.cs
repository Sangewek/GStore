using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameStore.DAL.Models
{
    public class GameGenres
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
