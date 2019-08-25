using System;
using System.Collections.Generic;

namespace ProjectQT.DataModel.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string FullName { get; set; }
        public DateTime BrithDay { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
