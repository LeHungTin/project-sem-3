using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.DataModel.Models
{
    public class ProductDetailOrder
    {
        [Key]
        public int Id { get; set; }
        public int OrderDeltailId { get; set; }
        public string NameAtt { get; set; }

        [ForeignKey("OrderDeltailId")]
        public OrderDetail OrderDetails { get; set; }
    }
}
