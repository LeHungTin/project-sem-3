using System;

namespace ProjectQT.DataModel.Models
{
   public class Attribute : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        public ProductAttribute ProductAttributes { get; set; }
    }
}
