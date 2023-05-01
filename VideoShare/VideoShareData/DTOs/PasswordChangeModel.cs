using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public class PasswordChangeModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string newPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Passwords must match.")]
        [MinLength(8)]
        public string confirmNewPassword { get; set; }
    }
}
