using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class Banner : BaseEntity
    {
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public Banner()
        {

        }
    }
   
}
