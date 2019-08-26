using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectQT.DataModel.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int HandelerId { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
