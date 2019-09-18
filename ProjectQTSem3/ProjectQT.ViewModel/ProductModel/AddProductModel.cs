using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.ProductModel
{
    public class AddProductModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public bool Status { set; get; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int AttributesId { get; set; }
        [Required]
        public string ProduceCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public long SalePrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
    }
}
