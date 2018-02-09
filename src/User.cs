using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class User
    {
        public string login { get; set; }
        public string displayName { get; set; }
        public List<Label> function { get; set; }
    }
}