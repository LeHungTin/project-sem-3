using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.DataModel.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateAt { set; get; }
        public int CreateBy { set; get; }
        public bool Status { set; get; }

        public ICollection<GroupRole> GroupRoles { get; set; }

    }
}
