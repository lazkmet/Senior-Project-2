using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.DTOs
{
    public class FileStreamRowInfo
    {
        public string? path { get; set; }
        public byte[] transactionContext { get; set; }
    }
}
