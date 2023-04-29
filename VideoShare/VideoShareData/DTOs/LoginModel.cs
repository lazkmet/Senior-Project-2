using System.ComponentModel.DataAnnotations;

namespace VideoShareData.DTOs
{
    public class LoginModel
    {
        [Required]
        //[EmailAddress] Commented out until test data is validated
        [MaxLength(254)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
