using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.DataModel.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateByEmail { set; get; }
        public int CreateBy { set; get; }
        public bool Status { set; get; }
    }
}
