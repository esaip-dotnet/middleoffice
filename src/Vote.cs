using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class Vote
    {
        public User author { get; set; }
        public DateTime timestamp { get; set; }
        public Answer answer { get; set; }
    }
}