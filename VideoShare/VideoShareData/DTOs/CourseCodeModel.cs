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
        [RegularExpression(@"^[A-Za-z0-9]*$", ErrorMessage = "Course Code can only contain alphanumeric characters")]
        [MaxLength(6, ErrorMessage = "Course Code must be 6 characters long")]
        [MinLength(6, ErrorMessage = "Course Code must be 6 characters long")]
        public string enteredCode { get; set; }
    }
}
