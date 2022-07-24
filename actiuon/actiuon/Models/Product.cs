using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Prise { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [NotMapped]
        public IFormFile ProdImg { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }



    }
}
