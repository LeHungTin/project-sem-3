using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.ViewModel.AttributeModel
{
    public class AttributeModel
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public string CreateBy { set; get; }
        public bool Status { set; get; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public string Value { get; set; }
    }
}
