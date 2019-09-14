using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.DataModel.Models
{
    public class ProductAttribute
    {
        [Column(Order =0),Key]
        public int ProductId { get; set; }
        [Column(Order = 1), Key]
        public int AttributeId { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }
        [ForeignKey("AttributeId")]
        public Attribute Attributes { get; set; }
    }
}
