# middleoffice
Middle Office avec les étudiants ESAIP IR 2018

# description
Ce code permet de mettre en place une API de vote pour attribuer une bourse de voyage ERASMUS. On peut saisir des demandes de vote, les visualiser et voter.

# api signature
POST /requests : ajoute une demande de vote
GET /requests : liste toutes les demandes en attente
GET /requests/{id} : affiche une demande pour lecture et vote éventuel
POST /requests/{id}/vote : donne un choix de vote pour une demande

# deploiement sur esaip.westeurope.cloudapp.azure.com
Connexion au SSH 22 avec user esaip / mot de passe donné en cours

# état du projet
En cours de développement

# licence
Licence MIT

# commandes Docker
docker build -t jpgouigoux/middleoffice .
docker run -d -p 80:80 --name jp jpgouigoux/middleoffice
docker rm -fv jp

# Comment réaliser les tests unitaires
Utilisez POSTMAN pour effectcuer les test d'envoie de requête POST, GET, etc.

Ajout d'une demande de vote :
Dans POSTMAN, envoyer le contenu du fichier "demande.json" (sans l'objet Vote) à l'adresse http://localhost:5000/api/Requests en POST. Doit retourner un status 201 Created.

Affichage d'une demande pour lecture / vote éventuel :
Dans POSTMAN, saisir la requête suivante  http://localhost:5000/api/Requests/{{idrequest}} en GET. Cela va renvoyer un JSON avec ' "vote": null ' à la fin. Doit retourner un status 200 OK.

Vote pour une demande :
Dans POSTMAN, saisir la requête suivante  http://localhost:5000/api/Requests/{{idrequest}}/Vote en POST avec l'objet json author, le timestamp et le code 0. Doit retourner un status 204 noContent

Visualiser la liste de toutes les demandes en attente :
Dans POSTMAN, saisir la requête suivante http://localhost:5000/api/Requests en GET. Celà doit retourner un status 200. 

# comment raporter un bug ou contribuer au projet
Si vous trouvez un ou plusieurs bug ou si vous souhaitez contribuer au projet, contactez  ebenaiteau.ir2018 at esaip.org