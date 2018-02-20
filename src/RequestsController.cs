using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MiddleOffice
{
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {
        //private static List<Request> db = new List<Request>();

        private const string urlService = "http://esaip.westeurope.cloudapp.azure.com/";

        private const string urlDatabase = "mongodb://esaip.westeurope.cloudapp.azure.com:27017";

        private MongoClient conn = new MongoClient(urlDatabase);

        private IMongoDatabase db
        {
            get { return conn.GetDatabase("ESAIP"); }
        }

        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {
            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();
            //db.Add(r);
            db.GetCollection<Request>("Demandes").InsertOne(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            //return new JsonResult(db.FindAll(r => r.vote == null));
            var list = db.GetCollection<Request>("Demandes").Find(_ => true).ToList();
            return new JsonResult(list);
        }

        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            //Request result = db.Find(r => r.id == id);
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (Request.Headers["Accept"] == "application/json")
                return new JsonResult(result);
            else
                //context.Response.Redirect();
                //return RedirectToPage("/vote.html");
                return Content("<html><body><h1>coucou</h1></body></html>");
        }

        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {
            //Request result = db.Find(r => r.id == id);
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            //db.GetCollection<Request>("Demandes").ReplaceOne(r => r.id == id, result);
            var update = Builders<Request>.Update.Set("vote", v);
            db.GetCollection<Request>("Demandes").UpdateOne(r => r.id == id, update);
            return new NoContentResult();
        }        
    }
}
