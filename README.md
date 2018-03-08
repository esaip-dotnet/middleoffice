# middleoffice
Middle Office avec les étudiants ESAIP IR 2018

# description
L'application Middle Office permet de créer des demandes et de voter pour ces dernières. L'application utilise une base de données MongoDB permettant d'enregistrer les demandes.

# api signature
- POST /requests : ajoute une demande de vote
- GET /requests : liste toutes les demandes en attente
- GET /requests/{id} : affiche une demande pour lecture et vote éventuel
- POST /requests/{id}/vote : donne un choix de vote pour une demande

# securité
La class HeaderAutorization permet de gérer l'identification à l'API
Elle contient la méthode FailFastCheckAuthorization qui permet de controller l'authentification en vérifiant le Basic Authentification du header HTTP Authorizarion
Basic authentification contient l'identifiant et le mot de passe encodé en base64
Valeur de retour de la méthode FailFastCheckAutorization :
- Retourne le couple true et username en cas de succès
- Retourne false et null dans les cas suivants :
	- si le header Authorization est null ou vide
	- s'il y a plusieurs header Authorization
	- si le header ne commence pas par "Basic"
- Retourne false et username dans les cas suivants :
	- si l'identifiant et le mot de passe est null
	- si l'identifiant ou le mot de passe est erroné

# configuration
Les variables à configurer se situent dans la class RequestController
urlService : URL de l'API
urlDatabase : URL de la base de données MongoDB
nameDatabase : nom de la base de données

# deploiement
La cible de déploiement est un CoreOS sur Azure, accessible sur `esaip.westeurope.cloudapp.azure.com`. La connexion au SSH 22 se fait avec le user `esaip` et le mot de passe donné en cours.

# test unitaire
Le répertoire "postman" contient l'ensemble des tests unitaires
- Créer une demande :
URL : http://localhost:5000/api/Requests
Method : POST
Code HTTP attendu : 201 Created

- Récupérer toutes les demandes
URL : http://localhost:5000/api/Requests
Method : GET
Code HTTP attendu : 200 OK
Réponse : liste contenant toutes les demandes aux format JSON

- Récupérer une demande :
URL : http://localhost:5000/api/Requests/{{idrequest}}
Method : GET
Code HTTP attendu : 200 OK
Réponse : demande au format JSON

- Voter pour une demande
URL : http://localhost:5000/api/Requests/{{idrequest}}/Vote
Method : POST
Code HTTP attendu : 204 No Content

# affectation des ports
- 80 : JP (Prof)
- 81 : Charles (ctrouplin / ctrouplin)
- 82 : Dorian (nairod95 / nairod)
- 83 : Alexandre (Lextoplasme / lextoplasme)
- 84 : Emilien (neilimebenaiteau / neilime1995)
- 85 : Quentin (qdenis / ?)
- 86 : Antoine (arichard44 / arichard)
- 87 : Benjamin (bsabaron / bsabaron)
- 88 : Clément (clembobo / clemboisse)

# commandes Docker
Pour compiler, c'est-à-dire créer une image à partir du Dockerfile dans le répertoire courant :

    docker build -t jpgouigoux/middleoffice .

Pour démarrer un conteneur à partir de l'image sur un port donné, avec un nom :

    docker run -d -p 80:80 --name jp jpgouigoux/middleoffice

Pour lister tous les conteneurs lancés (y compris ceux arrêtés) :

    docker ps -a

Pour afficher toutes les images disponibles dans le cache local :

	docker images

Pour afficher les logs d'un conteneur :

	docker logs jp

Pour supprimer complètement un conteneur, en forçant son arrêt et en supprimant ses données :

    docker rm -fv jp