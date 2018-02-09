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

        private string urlService = "http://esaip.westeurope.cloudapp.azure.com/";

        //POST /requests : ajoute une demande de vote
        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {
            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();
            db.Add(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        //GET /requests : liste toutes les demandes en attente
        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            return new JsonResult(db.FindAll(r => r.vote == null));
        }

        //GET /requests/{id} : affiche une demande pour lecture et vote éventuel
        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            Request result = db.Find(r => r.id == id);
            if (result == null) return NotFound();
            return new JsonResult(result);
        }

        //POST /requests/{id}/vote : donne un choix de vote pour une demande
        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {
            Request result = db.Find(r => r.id == id);
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            return new NoContentResult();
        }        
    }
}