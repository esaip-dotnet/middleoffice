using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class User
    {
        public String login { get; set; }
        public String displayName { get; set; }
        public List<Labels> function { get; set; }

    }
}