using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class User/*fondement de l'urbanisation */
    {
        public string login { get; set; }
        public string displayName { get; set; }
        public List<Label> fonction { get; set; }
    }
}
