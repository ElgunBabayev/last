using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class Category : BaseEntity

    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public List<Product> Product { get; set; }

        public Category(string Name)
        {
            this.Name = Name;
        }
        public Category()
        {
        }
    }
}
