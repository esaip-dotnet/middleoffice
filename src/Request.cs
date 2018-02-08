using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class Request
    {
        public string id { get; set; }
        public string summary { get; set; }
        public string payload { get; set; }
        public string category { get; set; }
        public List<Answer> answers { get; set; }
    }
}
