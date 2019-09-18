using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.AttributeModel
{
    public class GetAtrributeModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public bool Status { set; get; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Value { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
    }
}
