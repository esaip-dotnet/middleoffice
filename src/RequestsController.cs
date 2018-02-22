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
        // URL de pour se connecter à l'API
        private const string urlService = "http://esaip.westeurope.cloudapp.azure.com/";
        // URL de la base de donnée avec le port par défaut
        // TODO changer le port par défaut pour plus de sécurité
        private const string urlDatabase = "mongodb://esaip.westeurope.cloudapp.azure.com:27017";
        // Création du client Mongo
        private MongoClient conn = new MongoClient(urlDatabase);

        private IMongoDatabase db
        {
            get { return conn.GetDatabase("ESAIP"); }
        }

        // Méthode pour créer une demande
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

        // Méthode qui renvoie la liste des demandes de vote
        [HttpGet("/api/Requests")]
        public IActionResult GetRequests()
        {
            var list = db.GetCollection<Request>("Demandes").Find(_ => true).ToList();
            return new JsonResult(list);
        }

        // Méthode qui renvoie une demande en contion de l'id
        [HttpGet("/api/Requests/{id}")]
        public IActionResult GetRequest(string id)
        {
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (Request.Headers["Accept"] == "application/json")
                return new JsonResult(result);
            else
                return Content("<html><body><h1>coucou</h1></body></html>");
        }

        // Méthode pour éffectuer un vote. Prend en paamètre l'id de la demande de vote
        [HttpPost("/api/Requests/{id}/Vote")]
        public IActionResult Vote(string id, [FromBody] Vote v)
        {
            var result = db.GetCollection<Request>("Demandes").Find(r => r.id == id).FirstOrDefault();
            if (result == null) return NotFound();
            if (result.vote != null) return new BadRequestResult();
            result.vote = v;
            var update = Builders<Request>.Update.Set("vote", v);
            db.GetCollection<Request>("Demandes").UpdateOne(r => r.id == id, update);
            return new NoContentResult();
        }        
    }
}