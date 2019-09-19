
using System.ComponentModel.DataAnnotations;

namespace ProjectQT.ViewModel.AccountModel
{
     public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
