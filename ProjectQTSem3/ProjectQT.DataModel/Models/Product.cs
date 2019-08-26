using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public string ProduceCode { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public long SalePrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        public Order Orders { get; set; }
        public ProductAttribute ProductAttributes { get; set; }
        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }
    }
}
