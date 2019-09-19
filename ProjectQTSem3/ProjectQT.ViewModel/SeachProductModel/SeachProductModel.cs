using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.ViewModel.SeachProductModel
{
    public class SeachProductModel
    {
        public string ProductName { get; set; }
        public string StartPrice { get; set; }
        public string EndPrice { get; set; }
        public bool Reate1 { get; set; }
        public bool Reate2 { get; set; }
        public bool Reate3 { get; set; }
        public bool Reate4 { get; set; }
        public bool Reate5 { get; set; }
    }
}
