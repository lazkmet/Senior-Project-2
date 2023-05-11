using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShareData.Models;

public partial class CourseStudent : Interfaces.FileStreamDataObj
{
    public int CourseId { get; set; }

    public int UserId { get; set; }

    public Guid FileGuid { get; set; }
    [NotMapped]
    public byte[]? ProfilePicture { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public int? CompletionPercentage { get; set; }
    public string getTableName() { return "dbo.CourseStudents"; }
    public string getFilestreamColumn() { return "ProfilePicture"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
