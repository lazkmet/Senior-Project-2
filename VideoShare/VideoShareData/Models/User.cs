using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShareData.Models;

public partial class User : Interfaces.FileStreamDataObj
{
    public int UserId { get; set; }

    public Guid FileGuid { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [NotMapped]
    public byte[]? ProfilePicture { get; set; }

    public byte UserType { get; set; }

    public DateTime DateCreated { get; set; }

    public bool EmailVerified { get; set; }

    public byte WebsiteTheme { get; set; }

    public byte MinTextSize { get; set; }

    public byte CourseOrdering { get; set; }

    public byte StudentOrdering { get; set; }

    public byte[] EncryptedPassword { get; set; } = null!;

    public DateTime? LatestLogin { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<UserxCourse> UserxCourses { get; set; } = new List<UserxCourse>();

    public virtual ICollection<UserxVideo> UserxVideos { get; set; } = new List<UserxVideo>();
    public string getTableName() { return "dbo.Users"; }
    public string getFilestreamColumn() { return "ProfilePicture"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
