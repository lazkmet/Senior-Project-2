using System.ComponentModel.DataAnnotations;

namespace VideoShareData.DTOs
{
    public class NewUserModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string EmailAddress { get; set; }
        [Required]
        [Compare("EmailAddress", ErrorMessage = "Emails must match.")]
        [MaxLength(254)]
        public string ConfirmEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
