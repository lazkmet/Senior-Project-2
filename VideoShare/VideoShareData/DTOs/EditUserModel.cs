using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public class EditUserModel
    {
        [EmailAddress]
        [MaxLength(254)]
        public string EmailAddress { get; set; }
        [Compare("EmailAddress", ErrorMessage = "Emails must match.")]
        [MaxLength(254)]
        public string ConfirmEmail { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}
