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

# Comment réaliser les tests unitaires
Tester ajout d'une demande de vote :
Dans le logiciel POSTMAN, envoyer le contenu du fichier "demande.json" (sans l'objet Vote) à l'adresse URL :localhost:5000/api/Requests en POST. Doit retourner un status 201 Created et un (localhost:5000/api/Requests/<id>)

Tester l'affichage d'une demande pour lecture / vote éventuel :
Dans le logiciel POSTMAN, saisir la requête suivante URL : localhost:5000/api/Requests/<id> en GET. Cela va renvoyer un JSON avec ' "vote": null ' à la fin. Doit retourner un status 200 OK.

Tester le choix de vote pour une demande :
Dans le logiciel POSTMAN, saisir la requête suivante URL : localhost:5000/api/Requests/<id> en POST avec l'objet json author, le timestamp et le code 0. Doit retourner un status 204 noContent

Tester la visualisation de la liste de toutes les demandes en attente :
ans le logiciel POSTMAN, saisir la requête suivante URL : localhost:5000/api/Requests en GET. Celà doit retourner un status 200. 

    
