using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.ViewModel.AccountModel
{
    public class RegisterViewModel
    {
        public DateTime CreateAt { set; get; }

        public int CreateBy { set; get; }
        public bool Status { set; get; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 15 characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(31, MinimumLength =6, ErrorMessage = "FullName must be between 6 and 31 characters")]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
