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
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
