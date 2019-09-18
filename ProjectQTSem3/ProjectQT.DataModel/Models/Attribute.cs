using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
   public class Attribute : BaseEntity
    {
        
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Value { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        [ForeignKey("TypeId")]
        public TypeAttribute TypeAttribute { get; set; }

    }
}
