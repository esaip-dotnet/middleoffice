using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class Answer
    {
        public string code { get; set; }
        public string summary { get; set; }
        public string label { get; set; }
        public List<Header> headers { get; set; }
    }
}
