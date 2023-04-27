using System;
using System.Collections.Generic;

namespace VideoShareData.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int MessageType { get; set; }

    public string RecipientEmail { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? TimeSent { get; set; }

    public string? CourseCode { get; set; }

    public string? AdditionalText { get; set; }

    public virtual Course? CourseCodeNavigation { get; set; }
}
