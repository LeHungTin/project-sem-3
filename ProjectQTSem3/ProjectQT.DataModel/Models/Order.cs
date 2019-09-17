using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectQT.DataModel.Models
{
    public class Order
    {
        [Key]
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public int UserId { get; set; }
        public string HandelerId{ get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public bool Status { set; get; }


        [ForeignKey("UserId")]
        public User Users { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
