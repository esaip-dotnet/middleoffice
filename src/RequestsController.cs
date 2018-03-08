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
        // URL de l'API
        private const string urlService = "http://esaip.westeurope.cloudapp.azure.com/";
        
        // URL de la base de données
        private const string urlDatabase = "mongodb://database:27017";

        // Nom de le base de données
        private const string nameDatabase = "ESAIP";

        private MongoClient conn = new MongoClient(urlDatabase);

        private IMongoDatabase db
        {
            get { return conn.GetDatabase(nameDatabase); }
        }

        // Permet de créer une nouvelle requête
        [HttpPost]
        public IActionResult CreateRequest([FromBody] Request r)
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            Console.WriteLine("Création d'une demande");
            if (r == null) return new BadRequestResult();
            r.id = Guid.NewGuid().ToString();
            db.GetCollection<Request>("Demandes").InsertOne(r);
            return Created(urlService + "api/Requests/" + r.id, r);
        }

        // Permet de récupérer toutes les requêtes
        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            // Récupère toutes les demandes enregistrés sous forme de liste
            var list = db.GetCollection<Request>("Demandes").Find(_ => true).ToList();
            return new JsonResult(list);
        }

        // Permet de récupérer une requête par son id
        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            if (!HeaderAuthorization.FailFastCheckAuthorization(HttpContext).Item1)
                return StatusCode(403);

            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (Request.Headers["Accept"] == "application/json")
                return new JsonResult(result);
            else
                //context.Response.Redirect();
                //return RedirectToPage("/vote.html");
                return Content("<html><body><h1>coucou</h1></body></html>");
        }

        // Permet de voter pour une requête
        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {
            DateTime ts = DateTime.Now;

            Tuple<bool, string> resultatsAutorisation = HeaderAuthorization.FailFastCheckAuthorization(HttpContext);
            if (!resultatsAutorisation.Item1) return StatusCode(403);

            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            
            v.author = new User();
            v.author.login = resultatsAutorisation.Item2;
            v.author.displayName = resultatsAutorisation.Item2;
            v.timestamp = ts;

            // Enregistrement du vote
            var update = Builders<Request>.Update.Set("vote", v);
            db.GetCollection<Request>("Demandes").UpdateOne(r => r.id == id, update);
            return new NoContentResult();
        }        
    }
}
