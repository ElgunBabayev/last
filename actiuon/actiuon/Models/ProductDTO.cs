using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int MinPrice { get; set; }
        public string Status { get; set; }
        public DateTime EndTime { get; set; }
        public string UserName { get; set; }

    }
}
