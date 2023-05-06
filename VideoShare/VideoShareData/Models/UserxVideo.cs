using System;
using System.Collections.Generic;

namespace VideoShareData.Models;

public partial class UserxVideo
{
    public int UserId { get; set; }

    public int VideoId { get; set; }

    public bool VideoCompleted { get; set; }

    public int CurrentTime { get; set; }

    public DateTime? LastVisited { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
