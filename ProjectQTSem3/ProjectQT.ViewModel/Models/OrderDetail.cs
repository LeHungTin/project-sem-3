using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }

        [ForeignKey("OrderId")]
        public Order Orders { get; set; }

        [ForeignKey("ProductId")]
        public ICollection<Product> Products { get; set; }

    }
}
