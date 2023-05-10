using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public abstract class EditVideoModel
    {
        public int VideoId { get; set; } = -1; //Newly created videos have an ID of -1
        [Required]
        [MaxLength(50, ErrorMessage = "Video Title cannot be longer than 50 characters.")]
        public string VideoTitle { get; set; } = "";
        [MaxLength(300, ErrorMessage = "Video Title cannot be longer than 300 characters.")]
        public string VideoDescription { get; set; } = "";
        public short orderInCourse { get; set; }
        //public List<AttachmentModel> {get;set;} = new List<AttachmentModel>();
        public EditVideoModel() {}
        public EditVideoModel(EditVideoModel ev) {
            VideoId = ev.VideoId;
            VideoTitle = ev.VideoTitle;
            VideoDescription = ev.VideoDescription;
        }
    }
    public class YouTubeEditVideoModel : EditVideoModel {
        [RegularExpression(@"^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*", ErrorMessage = "Invalid Youtube URL.")]
        public string? YouTubeURL { get; set; } = null;
        public bool UseYTDescription { get; set; } = false;
        [Required(ErrorMessage = "Could not extract YouTube Video ID from provided URL")]
        public string? YouTubeID { get; set; }
        public YouTubeEditVideoModel() : base() {}
        public YouTubeEditVideoModel(YouTubeEditVideoModel yv) : base(yv) {
            YouTubeURL = yv.YouTubeURL;
            UseYTDescription = yv.UseYTDescription;
            YouTubeID = yv.YouTubeID;
        }
        public void GetIDFromURL() {
            //Youtube URLs follow a specific pattern, with an 11-character video ID
            if (string.IsNullOrEmpty(YouTubeURL)) { return; }
            Regex URLRegex = new Regex(@"^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*");
            var match = URLRegex.Match(YouTubeURL);
            if (match.Success) {
                var groups = match.Groups;
                if (groups.Count > 2 && groups[2].Length == 11) {
                    YouTubeID = groups[2].Value;
                    return;
                }
            }
            YouTubeID = null;
            return;
        }
    }
    /*
    public class UploadedEditVideoModel : EditVideoModel {
        TO DO
    }
    */
}
