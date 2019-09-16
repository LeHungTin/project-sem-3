using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.ReportModel
{
    public class OrderReport
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { set; get; }
    }
}
