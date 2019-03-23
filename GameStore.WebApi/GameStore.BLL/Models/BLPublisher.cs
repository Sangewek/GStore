using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Models
{
    public class BLPublisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BLGame> Games { get; set; }
    }
}
