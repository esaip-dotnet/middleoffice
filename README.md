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


# Test unitaire

POST /requests : ajoute une demande de vote
Url http://esaip.westeurope.cloudapp.azure.com/api/Requests
Method : Post
(mettre tout le json mais retirer la partie "vote")


liste toutes les demandes en attente
Url http://esaip.westeurope.cloudapp.azure.com/api/Requests
Method : Get

Obtenir tous les votes :
Url http://esaip.westeurope.cloudapp.azure.com/api/Requests
Method : Get

affiche une demande pour lecture et vote éventuel
Url http://esaip.westeurope.cloudapp.azure.com/api/Requests/{id}
Method : Get
Test : var jsonData = JSON.parse(responseBody);
    pm.globals.set("idrequest",jsonData.id);

donne un choix de vote pour une demande
Url http://esaip.westeurope.cloudapp.azure.com/api/Requests/{id}/Vote
Method : Post
Mettre la partie "vote" du json
        