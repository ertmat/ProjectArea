using System.ComponentModel.DataAnnotations;

namespace ProjectArea.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

        public string RedirectUrl { get; set; }
    }
}
