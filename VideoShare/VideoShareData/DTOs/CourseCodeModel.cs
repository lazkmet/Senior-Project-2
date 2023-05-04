using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public class CourseCodeModel
    {
        [Required]
        [RegularExpression(@"^[A-Z0-9]*$", ErrorMessage = "Invalid Characters Present")]
        [MaxLength(6)]
        [MinLength(6)]
        public string enteredCode { get; set; }
    }
}
