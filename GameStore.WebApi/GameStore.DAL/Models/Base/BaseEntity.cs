using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.DAL.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
