using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameStore.DAL.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }

        public virtual Publisher Publisher {get;set;}
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GameGenres> GameGenres { get; set; }
        public virtual ICollection<GamePlatforms> GamePlatform { get; set; }

    }
}
