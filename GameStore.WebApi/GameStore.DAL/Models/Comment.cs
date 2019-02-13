using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameStore.DAL.Models
{
    
    public class Comment 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
    }
}
