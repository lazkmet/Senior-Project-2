using System;
using System.Collections.Generic;
using VideoShareData.Enums;

namespace VideoShareData.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public MessageType MessageType { get; set; }

    public string RecipientEmail { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? TimeSent { get; set; }

    public string? CourseCode { get; set; }

    public string? AdditionalText { get; set; }
}
