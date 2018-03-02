using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    //Classe constructeur Action
    public class Action 
    {
        public string url { get; set; }
        public string verb { get; set; }
        public List<Header> headers { get; set; }
        public string payload { get; set; }
    }
}