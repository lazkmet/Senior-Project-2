using System;
using System.Collections.Generic;

namespace VideoShareData.Models;

public partial class AllUser
{
    public int UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateJoined { get; set; }

    public int? CoursesOwned { get; set; }

    public int? CoursesJoined { get; set; }
}
