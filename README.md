# middleoffice
Middle Office avec les étudiants ESAIP IR 2018

# api signature
POST /requests : ajoute une demande de vote
GET /requests : liste toutes les demandes en attente
GET /requests/{id} : affiche une demande pour lecture et vote éventuel
POST /requests/{id}/vote : donne un choix de vote pour une demande

# deploiement sur esaip.westeurope.cloudapp.azure.com
Connexion au SSH 22 avec user esaip / mot de passe donné en cours

# affectation des ports
80 : JP (Prof)
81 : Charles
82 : Dorian
83 : Alexandre
84 : Emilien
85 : Quentin
86 : Antoine
87 : Benjamin
88 : Clément

# commandes Docker
docker build -t jpgouigoux/middleoffice .
docker run -d -p 80:80 --name jp jpgouigoux/middleoffice
docker rm -fv jp

# test unitaire
Créer une demande :
URL : http://localhost:5000/api/Requests
Method : POST
Body : raw -> JSON (joindre demande.json)
Tests :
    var jsonData = JSON.parse(responseBody);
    pm.globals.set("idrequest", jsonData.id)
Résultat : 201 Created

Récupérer une demande :
URL : http://localhost:5000/api/Requests/{{idrequest}}
Method : GET
Résultat : 200 OK

Récupérer toutes les demandes
URL : http://localhost:5000/api/Requests
Method : GET
Résultat : 200 OK

Voter pour une demande
URL : http://localhost:5000/api/Requests/{{idrequest}}/Vote
Method : POST
Body : raw -> JSON (joindre objet vote)
Résultat : 204 No Content

# configuration
Modifier la variable urlService dans la classe RequestsController