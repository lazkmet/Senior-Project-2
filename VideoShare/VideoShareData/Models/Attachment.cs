using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VideoShareData.Interfaces;

namespace VideoShareData.Models;

public partial class Attachment : Interfaces.FileStreamDataObj
{
    public int VideoId { get; set; }

    public string FileName { get; set; } = null!;

    public Guid FileGuid { get; set; }

    [NotMapped]
    public byte[]? FileData { get; set; }

    public DateTime UploadDate { get; set; }

    public virtual Video Video { get; set; } = null!;

    public string getTableName() { return "dbo.Attachments"; }
    public string getFilestreamColumn() { return "FileData"; }
    public string getFileGUID() { return FileGuid.ToString(); }
}
