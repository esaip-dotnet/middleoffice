using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleOffice
{
    public class Vote
    {
        public User author { get; set; }
        public DateTime timestamp { get; set; }/*possible évolution du format json */
        public Answer answer { get; set; }/*récupération du code voté uniquement (les autres champs sont null) */
    }
}
