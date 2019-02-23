using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebApi.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
