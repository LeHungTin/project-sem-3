using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.DataModel.Models
{
    public class TypeAttribute : BaseEntity
    {
        [Required]
        public string TypeName { get; set; }

        public ICollection<Attribute> Attributes { get; set; }

    }
}
