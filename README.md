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
    - Test ajout d'une demande de vote :
    Dans POSTMAN, envoyer "demande.json" (sans l'objet Vote) à l'adresse localhost:5000/. Cela retournera le code 201 et un id de type localhost:5000/api/Requests/<id>
    
