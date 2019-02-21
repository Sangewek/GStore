using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GameStore.DAL.Models.Base;

namespace GameStore.DAL.Models
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
