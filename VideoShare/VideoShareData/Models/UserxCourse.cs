using System;
using System.Collections.Generic;

namespace VideoShareData.Models;

public partial class UserxCourse
{
    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime DateJoined { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
