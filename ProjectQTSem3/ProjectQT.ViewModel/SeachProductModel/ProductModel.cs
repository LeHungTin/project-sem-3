using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.ProductModel
{
    public class ProductModel
    {
        public int Id { set; get; }
        public string CategoryId { get; set; }
        public string AttributesId { get; set; }
        public string ProduceCode { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float? Reate { get; set; }
        public int? CountBuy { get; set; }
        public int? CountView { get; set; }
    }
}
