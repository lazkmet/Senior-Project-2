using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public class StyleProperties
    {
        public string? Height { get; set; }
        public string? Width { get; set; }
        public string? MaxHeight { get; set; }
        public string? MaxWidth { get; set; }
        public string? MinHeight { get; set; }
        public string? MinWidth { get; set; }
        public string GetStyle() {
            string styleString = "";
            if (!String.IsNullOrWhiteSpace(Height)) {
                styleString += "height:" + Height + ";";
            }
            if (!String.IsNullOrWhiteSpace(Width))
            {
                styleString += "width:" + Width + ";";
            }
            if (!String.IsNullOrWhiteSpace(MaxHeight))
            {
                styleString += "max-height:" + MaxHeight + ";";
            }
            if (!String.IsNullOrWhiteSpace(MaxWidth))
            {
                styleString += "max-width:" + MaxWidth + ";";
            }
            if (!String.IsNullOrWhiteSpace(MinHeight))
            {
                styleString += "min-height:" + MinHeight + ";";
            }
            if (!String.IsNullOrWhiteSpace(MinWidth))
            {
                styleString += "min-width:" + MinWidth + ";";
            }
            return styleString;
        }
    }
}
