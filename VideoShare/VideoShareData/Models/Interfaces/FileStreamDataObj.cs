using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.Models.Interfaces
{
    internal interface FileStreamDataObj
    {
        public string getTableName();
        public string getFilestreamColumn();
        public string getFileGUID();
    }
}
