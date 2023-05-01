using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoShareData.Enums;

namespace VideoShareData.Models;

public partial class Course : Interfaces.FileStreamDataObj
{
    public int CourseId { get; set; }

    public Guid FileGuid { get; set; }

    public int OwnerId { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string? CourseDescription { get; set; }

    [NotMapped]
    public byte[]? CourseImage { get; set; }

    public LessonLimitType LessonLimitType { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<UserxCourse> UserxCourses { get; set; } = new List<UserxCourse>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    public string getTableName() { return "dbo.Courses"; }
    public string getFilestreamColumn() { return "CourseImage"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
