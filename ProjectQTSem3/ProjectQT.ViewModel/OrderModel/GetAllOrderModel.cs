using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.OrderModel
{
    public class GetAllOrderModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public bool Status { set; get; }
        public string UserId { get; set; }
        public string HandelerId { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
    }
}
