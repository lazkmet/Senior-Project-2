using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.Services
{
    public class ServiceTaskResults<T>
    {
        public bool TaskSuccessful { get; set; } = false;
        public string TaskMessage { get; set; } = "No message provided.";
        public T? ReturnValue { get; set; } = default(T);
    }
}
