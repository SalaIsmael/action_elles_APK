# action_elles_APK

## Description
Cette API REST permet la gestion des souscriptions à une assurance automobile.  
Elle permet aux utilisateurs de simuler une prime d'assurance et de finaliser une souscription.  

### Technologies Utilisées
- **.NET 9** - Framework backend moderne et performant.
- **Entity Framework Core (EF Core)** - ORM pour la gestion de la base de données.
- **PostgreSQL** - Base de données relationnelle robuste.
- **Docker & Docker Compose** - Conteneurisation de l'API et de la base de données.

## Installation et Lancement
### Prérequis
- **Docker** (dernière version)
- **.NET SDK 9**


###  Cloner le projet
```sh
git clone https://github.com/SalaIsmael/action_elles_APK.git
cd action_elles_APK
```
### Lancer l'API avec Docker
```sh
docker-compose up --build -d