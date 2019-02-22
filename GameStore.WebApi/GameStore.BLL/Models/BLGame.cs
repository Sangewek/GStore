using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models
{
    public class BLGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PublisherId { get; set; }
        public ICollection<BLComment> Comments { get; set; }
        public ICollection<BLGenre> Genres { get; set; }
        public ICollection<BLPlatform> Platforms { get; set; }
    }
}
