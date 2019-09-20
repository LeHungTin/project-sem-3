using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public int CreateBy { set; get; }
        public bool Status { set; get; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        [ForeignKey("OrderId")]
        public Order Orders { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public ICollection<ProductDetailOrder> ProductDetailOrders { get; set; }


    }
}
