using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GameStore.DAL.Models.Base;

namespace GameStore.DAL.Models
{
    
    public class Comment: BaseEntity
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public int GameId { get; set; }
        [ForeignKey("ParentComment")]
        public int? ParentCommentId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
    }
}
