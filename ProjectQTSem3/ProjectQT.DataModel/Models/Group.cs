using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.DataModel.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]

        public string GroupName { get; set; }
        public DateTime CreateAt { set; get; }
        public int CreateBy { set; get; }
        public bool Status { set; get; }

        public ICollection<User> Users { get; set; }
        public ICollection<GroupRole> GroupRoles { get; set; }

    }
}
