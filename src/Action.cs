using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class Action
    {
        public string url { get; set; }
        public string verb { get; set; }
        public object headers { get; set; }
        public object payload { get; set; }
        public List<Answer> answers { get; set; }
    }
}