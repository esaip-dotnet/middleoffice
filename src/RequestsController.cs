using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace MiddleOffice
{
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {
        [HttpGet("/")]
        public IActionResult Racine()
        {
            return new ObjectResult("OK, ça marche !");
        }

        [HttpPost]
        public void CreateRequest([FromBody] Request r)
        {
            Console.WriteLine("Création d'une demande");
        }
    }
}
