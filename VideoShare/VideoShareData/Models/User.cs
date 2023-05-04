using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoShareData.Enums;

namespace VideoShareData.Models;

public partial class User : Interfaces.FileStreamDataObj
{
    public int UserId { get; set; }

    public Guid FileGuid { get; set; }

    public string EmailAddress { get; set; } = null!;
    [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
    public string? FirstName { get; set; }
    [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
    public string? LastName { get; set; }

    //Full Name: If both are null, return null. Else, return full name
    [NotMapped]
    public string FullName { get { return (FirstName is null && LastName is null) ? "" : ($"{(FirstName is null ? "" : FirstName)} {(LastName is null ? "" : LastName)}"); } }

    [NotMapped]
    public byte[]? ProfilePicture { get; set; }

    public UserType UserType { get; set; }

    public DateTime DateCreated { get; set; }

    public bool EmailVerified { get; set; }

    public WebsiteTheme WebsiteTheme { get; set; }

    public byte MinTextSize { get; set; }

    public SortOrder CourseOrdering { get; set; }

    public SortOrder StudentOrdering { get; set; }

    public byte[] EncryptedPassword { get; set; } = null!;

    public DateTime? LatestLogin { get; set; }

    //Note: This can be used to hold "Courses Owned" or "Courses Joined" depending on the page.
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<UserxCourse> UserxCourses { get; set; } = new List<UserxCourse>();

    public virtual ICollection<UserxVideo> UserxVideos { get; set; } = new List<UserxVideo>();

    public string getTableName() { return "dbo.Users"; }
    public string getFilestreamColumn() { return "ProfilePicture"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
