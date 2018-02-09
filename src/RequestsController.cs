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
        private static List<Request> db = new List<Request>();
        private String urlService = "https://esaip.westeurope.cloudapp.azure.com/";
        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {//création de la demande
            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();
            db.Add(r);
            return Created(urlService + "api/Request/" + r.id, r);
        }

        [HttpGet("/api/Requests/")]
        public IActionResult GetRequests()
        {//liste des requests
            return new JsonResult(db.FindAll(r => r.vote == null));
        }

        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(String id)
        {//récup' d'une request by id
            Request result = db.Find(r => r.id == id);
            if (result == null) return NotFound();
            return new JsonResult(result);
        }
        
        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult vote(String id, [FromBody] Vote v)
        {//récup' du vote
            Request result = db.Find(r => r.id == id);
            result.id = Guid.NewGuid().ToString();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            return new NoContentResult();// on pourrais renvoyer un contenu du type String "le vote a été enregistré"
        }

    }
}