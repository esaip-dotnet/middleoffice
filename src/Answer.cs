using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    // Classe constructeur Answer
    public class Answer
    {
        public string code { get; set; }
        public List<Label> summary { get; set; }
        public List<Label> content { get; set; }
        public Action action { get; set; }
    }
}