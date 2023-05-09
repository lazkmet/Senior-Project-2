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
        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; } = "";
        [MaxLength(500)]
        public string CourseDescription { get; set; } = "";
        [Required]
        public LessonLimitType LessonLimitType { get; set; } = LessonLimitType.None;
        [Required]
        [MustHaveOneElement(ErrorMessage = "Course must have at least one video.")]
        public List<EditVideoModel> videos = new List<EditVideoModel>();
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
