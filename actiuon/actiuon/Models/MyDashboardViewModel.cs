using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class MyDashboardViewModel
    {

        public MyDashboardViewModel()
        {
            Products = new List<Product>();
        }
        public  AppUser AppUser { get; set; }
        public List<Category> Categories { get; set; }
        public  List<Product> Products { get; set; }
        public IFormFile ProdImg { get; set; }
        public List<Status> Status { get; set; }
        public int StatusId { get; set; }

    }
}
