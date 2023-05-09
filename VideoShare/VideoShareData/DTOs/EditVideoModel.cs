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
        [Required]
        public int VideoId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Video Title cannot be longer than 50 characters.")]
        public string VideoTitle { get; set; } = "";
        [Required]
        [MaxLength(300, ErrorMessage = "Video Title cannot be longer than 300 characters.")]
        public string VideoDescription { get; set; } = "";
        //public List<AttachmentModel> {get;set;} = new List<AttachmentModel>();
    }
    public class YouTubeEditVideoModel : EditVideoModel {
        [Required]
        [RegularExpression(@"^.* (youtu\.be\/| v\/| u\/\w\/| embed\/| watch\?v =|\&v =)([^#\&\?]*).*", ErrorMessage = "Invalid Youtube URL.")]
        public string YouTubeURL { get; set; }
        public bool UseYTDescription { get; set; } = false;
        [Required]
        public string? YouTubeID { get; set; }
        public void GetIDFromURL() {
            //Youtube URLs follow a specific pattern, with an 11-character video ID
            if (string.IsNullOrEmpty(YouTubeURL)) { return; }
            Regex URLRegex = new Regex(@"^.* (youtu\.be\/| v\/| u\/\w\/| embed\/| watch\?v =|\&v =)([^#\&\?]*).*");
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
