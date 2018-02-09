# middleoffice
Middle Office avec les étudiants ESAIP IR 2018

# api signature
- POST /requests : ajoute une demande de vote
- GET /requests : liste toutes les demandes en attente
- GET /requests/{id} : affiche une demande pour lecture et vote éventuel
- POST /requests/{id}/vote : donne un choix de vote pour une demande

# deploiement
La cible de déploiement est un CoreOS sur Azure, accessible sur `esaip.westeurope.cloudapp.azure.com`. La connexion au SSH 22 se fait avec le user `esaip` et le mot de passe donné en cours.

# affectation des ports
- 80 : JP (Prof)
- 81 : Charles
- 82 : Dorian
- 83 : Alexandre
- 84 : Emilien
- 85 : Quentin
- 86 : Antoine
- 87 : Benjamin
- 88 : Clément

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