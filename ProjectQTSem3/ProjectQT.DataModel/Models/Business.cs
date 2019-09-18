using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.DataModel.Models
{
    public class Business
    {
        [Key]
        public string BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool Status { get; set; }

        public ICollection<GroupRole> GroupRoles { get; set; }

    }
}
