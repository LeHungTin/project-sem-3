using System;
using System.Collections.Generic;
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
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
