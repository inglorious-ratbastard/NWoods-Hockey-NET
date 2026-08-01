using System.ComponentModel.DataAnnotations;

namespace MvcSample.Models.ViewModels

{
    public class LoginRegisterViewModel
    {
        [EmailAddress]
        public string LoginEmail { get; set; }

        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        public bool RememberMe { get; set; }

        [Required]
        [EmailAddress]
        public string RegisterEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }

        [Compare(nameof(RegisterPassword))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
