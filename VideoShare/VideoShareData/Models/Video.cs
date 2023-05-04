using System;
using System.Collections.Generic;
using VideoShareData.Enums;

namespace VideoShareData.Models;

public partial class Video : Interfaces.FileStreamDataObj
{
    public int VideoId { get; set; }

    public Guid FileGuid { get; set; }

    public int CourseId { get; set; }

    public string VideoTitle { get; set; } = null!;

    public string? VideoDescription { get; set; }

    public short OrderInCourse { get; set; }

    public VideoType VideoType { get; set; }

    public string? YtvideoId { get; set; }

    public bool YtuseDescription { get; set; }

    public byte[]? VideoData { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<UserxVideo> UserxVideos { get; set; } = new List<UserxVideo>();

    public string getTableName() { return "dbo.Videos"; }
    public string getFilestreamColumn() { return "VideoData"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
