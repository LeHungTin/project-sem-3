using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class Product : BaseEntity
    {

        public int CategoryId { get; set; }
        [Required]

        public string ProduceCode { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string ImageUrl { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public float? Reate { get; set; }
        public int? CountBuy { get; set; }
        public int? CountView { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categorie { get; set; }
    }
}
