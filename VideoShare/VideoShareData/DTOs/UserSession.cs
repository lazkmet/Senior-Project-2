using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShareData.Enums;

namespace VideoShareData.DTOs
{
    public class UserSession
    {
        public int Id { get; set; }
        public UserType Role { get; set; }
        public string FullName { get; set; }
        public string? PfpFilepath { get; set; }
    }
}
