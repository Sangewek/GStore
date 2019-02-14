using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models
{
    public class BLComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
