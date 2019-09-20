using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.UserModel
{
    public class UpdateUserModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public bool Status { set; get; }
        public int GroupId { get; set; }
        [Required]

        public string FullName { get; set; }
        [Required]

        public DateTime BirthDay { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]

        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
