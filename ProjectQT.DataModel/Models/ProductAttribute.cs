using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class ProductAttribute : BaseEntity
    {
        public int ProductId { get; set; }
        public int AtrributeId { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }
        [ForeignKey("AtrributeId")]
        public Attribute Attributes { get; set; }
    }
}
