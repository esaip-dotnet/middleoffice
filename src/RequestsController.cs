using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;

namespace MiddleOffice
{
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {       
        private static List<Request> db = new List<Request>();

        private string urlService = "esaip.westeurope.cloudapp.azure.com/";
        
        MongoClient client = new MongoClient("mongodb://esaip.westeurope.cloudapp.azure.com:27017");

        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {
            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();
            var db = client.GetDatabase("ESAIP");
            db.GetCollection<Request>("Demandes").InsertOne(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            var db = client.GetDatabase("ESAIP");
            var list = db.GetCollection<Request>("Demandes").Find(_ => true).ToList();
            return new JsonResult(list);
        }

        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            var db = client.GetDatabase("ESAIP");
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).First();
            return new JsonResult(result);
        }

        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {
            var db = client.GetDatabase("ESAIP");
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).First();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            var update = Builders<Request>.Update.Set("vote", v);
            db.GetCollection<Request>("Demandes").UpdateOne(r => r.id == id, update);
            return new NoContentResult();
        }        
    }
}