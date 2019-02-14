﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameStore.DAL.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("BaseGenre")]
        public int? BaseGenreId { get; set; }

        public virtual Genre BaseGenre { get; set; }
        public virtual ICollection<GameGenres> GameGenres { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; }
    }
}
