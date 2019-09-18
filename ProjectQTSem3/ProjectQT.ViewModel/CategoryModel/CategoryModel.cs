using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.ViewModel.CategoryModel
{
    public class CategoryModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy{ set; get; }
        public bool Status { set; get; }
        [Required]
        public string Name { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy{ get; set; }


    }
}
