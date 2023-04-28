using System;
using System.Collections.Generic;

namespace VideoShareData.Models;

public partial class AllCourse
{
    public int CourseId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? NumStudents { get; set; }

    public DateTime DateCreated { get; set; }
}
