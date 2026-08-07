using System.ComponentModel.DataAnnotations;

namespace MvcSample.Models.ViewModels

{
    public class LoginRegisterViewModel
    {
        [Required]
        public string RegisterUserName { get; set; }

        [Required]
        [EmailAddress]
        public string RegisterEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }

        [Compare(nameof(RegisterPassword))]
        public string ConfirmPassword { get; set; }


        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
