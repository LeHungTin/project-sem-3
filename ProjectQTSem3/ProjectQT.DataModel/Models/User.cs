using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class User
    {
        [Key]
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public bool Status { set; get; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }

        [ForeignKey("GroupId")]
        public Group Groups { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
