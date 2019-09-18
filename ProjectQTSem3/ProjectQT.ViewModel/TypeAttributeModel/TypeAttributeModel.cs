using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.TypeAttributeModel
{
    public class TypeAttributeModel
    {
        public int Id { set; get; }
        public string TypeName { get; set; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public bool Status { set; get; }
    }
}
