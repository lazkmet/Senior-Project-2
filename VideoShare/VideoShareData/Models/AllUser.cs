using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShareData.Models;

public partial class AllUser
{
    public int UserId { get; set; }

    public string EmailAddress { get; set; } = null!;
    [DisplayFormat(NullDisplayText = "")]
    public string? FirstName { get; set; }
    [DisplayFormat(NullDisplayText = "")]
    public string? LastName { get; set; }
    //Full Name: If both are null, return null. Else, return full name
    [NotMapped]
    public string FullName { get { return (FirstName is null && LastName is null) ? "" : ($"{(FirstName is null ? "" : FirstName)} {(LastName is null ? "" : LastName)}"); } }
    public DateTime DateJoined { get; set; }

    public int? CoursesOwned { get; set; }

    public int? CoursesJoined { get; set; }
}
