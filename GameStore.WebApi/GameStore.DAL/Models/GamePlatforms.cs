using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.DAL.Models
{
    public class GamePlatforms
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int PlatformId { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
