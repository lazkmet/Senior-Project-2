using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoShareData.Models;

public partial class AllCourse
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }

    [DisplayFormat(NullDisplayText = "")]
    public string? FirstName { get; set; }

    [DisplayFormat(NullDisplayText = "")]
    public string? LastName { get; set; }
    //Full Name: If both are null, return null. Else, return full name
    [NotMapped]
    public string FullName { get { return (FirstName is null && LastName is null) ? "" : ($"{(FirstName is null ? "" : FirstName)} {(LastName is null ? "" : LastName)}"); } }
    public int? NumStudents { get; set; }
    public int? NumVideos { get; set; }
    public DateTime DateCreated { get; set; }
}
