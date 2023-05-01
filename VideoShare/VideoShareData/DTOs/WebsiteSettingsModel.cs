using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShareData.Enums;

namespace VideoShareData.DTOs
{
    public class WebsiteSettingsModel
    {
        public WebsiteTheme userTheme { get; set; }
        public int minTextSize { get; set; }
    }
}
