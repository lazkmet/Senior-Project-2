using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using VideoShareData.Enums;

namespace VideoShareData.DTOs
{
    public class EditCourseModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course Title is required.")]
        [MaxLength(100, ErrorMessage = "Course Title cannot be longer than 100 characters.")]
        public string CourseName { get; set; } = "";
        [MaxLength(500, ErrorMessage = "Course Description cannot be longer than 500 characters.")]
        public string CourseDescription { get; set; } = "";
        [Required]
        [EnumDataType(typeof(LessonLimitType))]
        public LessonLimitType LessonLimitType { get; set; } = LessonLimitType.None;
        [Required]
        [MustHaveOneElement(ErrorMessage = "Course must have at least one video.")]
        [ValidateComplexType]
        public List<EditVideoModel> videos { get; set; } = new List<EditVideoModel>();
        public List<EditVideoModel> deletedVideos = new List<EditVideoModel>();
    }
    //From https://stackoverflow.com/questions/13361500/array-must-contain-1-element
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
