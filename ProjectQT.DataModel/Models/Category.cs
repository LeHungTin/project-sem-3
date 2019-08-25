using System.Collections.Generic;

namespace ProjectQT.DataModel.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        //public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
