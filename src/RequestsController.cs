using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace MiddleOffice
{
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {

        private static List<Request> db = new List<Request>();

        private string urlService = "http://esaip.westeurope.cloudapp.azure.com/";

        //private static List<Request> db = new List<Request>();

        private const string urlService = "http://esaip.westeurope.cloudapp.azure.com/";

        private const string urlDatabase = "mongodb://database:27017";

        private MongoClient conn = new MongoClient(urlDatabase);

        private IMongoDatabase db
        {
            get { return conn.GetDatabase("ESAIP"); }
        }


        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();

            db.Add(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            return new JsonResult(db.FindAll(r => r.vote == null));
        }

        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            Request result = db.Find(r => r.id == id);
            //db.Add(r);
            db.GetCollection<Request>("Demandes").InsertOne(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            //return new JsonResult(db.FindAll(r => r.vote == null));
            var list = db.GetCollection<Request>("Demandes").Find(_ => true).ToList();
            return new JsonResult(list);
        }

        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            //Request result = db.Find(r => r.id == id);
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();

            if (result == null) return NotFound();
            if (Request.Headers["Accept"] == "application/json")
                return new JsonResult(result);
            else
                //context.Response.Redirect();

                return RedirectToPage("/vote.html");
                //return Content("<html><body><h1>coucou</h1></body></html>");
                //return RedirectToPage("/vote.html");
                return Content("<html><body><h1>coucou</h1></body></html>");

        }

        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {

            Request result = db.Find(r => r.id == id);
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            DateTime ts = DateTime.Now;

            Tuple<bool, string> resultatsAutorisation = HeaderAuthorization.FailFastCheckAuthorization(HttpContext);
            if (!resultatsAutorisation.Item1) return StatusCode(403);

    //"author" : {
    //     "login" : "kermorvant-a",
    //     "displayName" : "Armel Kermorvant",
    //     "function" : [{
    //         "lang" : "fr-Fr",
    //         "value" : "Directeur de formation"
    //     }]
    // },
    // "timestamp" : "09/02/2018 13:14:55",

            //Request result = db.Find(r => r.id == id);
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            
            v.author = new User();
            v.author.login = resultatsAutorisation.Item2;
            v.author.displayName = resultatsAutorisation.Item2;
            v.timestamp = ts;

            //db.GetCollection<Request>("Demandes").ReplaceOne(r => r.id == id, result);
            var update = Builders<Request>.Update.Set("vote", v);
            db.GetCollection<Request>("Demandes").UpdateOne(r => r.id == id, update);
            return new NoContentResult();
        }        
    }
}
