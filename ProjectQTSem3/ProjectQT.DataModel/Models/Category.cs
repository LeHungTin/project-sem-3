using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.DataModel.Models
{
    public class Category : BaseEntity
    {
        [Required]

        public string Name { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
