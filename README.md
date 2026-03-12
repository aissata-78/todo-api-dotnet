# 📝 Todo API — ASP.NET Core + MongoDB + JWT

**Étudiant(e) :** Aissata SY  
**Classe :** M2GLSI  
**Université :** UCAD — École Supérieure Polytechnique  
**Cours :** Enseignement .NET — 2025/2026

---

## 🎯 Description

API REST construite avec **ASP.NET Core 8**, **MongoDB** et sécurisée par **JWT (JSON Web Tokens)**.

### Fonctionnalités
- CRUD complet sur les Todos
- Authentification par token JWT
- Gestion des rôles : **admin** et **user**
- Documentation interactive via **Swagger UI**

### Règles de sécurité
| Action | Rôle requis |
|--------|------------|
| Lire les todos (GET) | `user` ou `admin` |
| Créer un todo (POST) | `admin` uniquement |
| Modifier un todo (PUT) | `admin` uniquement |
| Supprimer un todo (DELETE) | `admin` uniquement |

---

## 🚀 Lancer l'application avec Docker

### Prérequis
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installé

### Étapes

```bash
# 1. Cloner le repository
git clone https://github.com/VOTRE_USERNAME/todo-api-dotnet.git
cd todo-api-dotnet

# 2. Lancer l'application (API + MongoDB)
docker compose up --build

# 3. Ouvrir Swagger dans le navigateur
# → http://localhost:8080
```

Pour arrêter :
```bash
docker compose down
```

---

## 🧪 Tester l'API (via Swagger)

### Étape 1 — Créer un compte admin

`POST /api/auth/register`

```json
{
  "fullName": "Aissata SY",
  "email": "admin@todo.com",
  "password": "Admin123!",
  "role": "admin"
}
```

### Étape 2 — Se connecter

`POST /api/auth/login`

```json
{
  "email": "admin@todo.com",
  "password": "Admin123!"
}
```

→ Copier le `token` dans la réponse.

### Étape 3 — S'authentifier dans Swagger

Cliquer sur le bouton **Authorize 🔒** en haut de Swagger et entrer :
```
Bearer VOTRE_TOKEN_ICI
```

### Étape 4 — Créer un Todo (admin requis)

`POST /api/todos`

```json
{
  "title": "Finir le TP .NET",
  "description": "Rendre le TP avant dimanche",
  "isCompleted": false
}
```

### Étape 5 — Créer un compte user simple

`POST /api/auth/register`

```json
{
  "fullName": "User Test",
  "email": "user@todo.com",
  "password": "User123!",
  "role": "user"
}
```

Ce compte pourra **lire** les todos mais **pas les modifier**.

---

## 📁 Structure du projet

```
TodoApi/
├── Controllers/
│   ├── AuthController.cs      # Register + Login → token JWT
│   └── TodosController.cs     # CRUD todos (protégé par rôles)
├── Models/
│   ├── Todo.cs                # Modèle Todo (MongoDB)
│   ├── ApplicationUser.cs     # Utilisateur Identity
│   ├── ApplicationRole.cs     # Rôle Identity
│   ├── AuthDtos.cs            # DTOs pour Register/Login
│   └── MongoDbSettings.cs     # Config MongoDB
├── Services/
│   ├── TodoService.cs         # Accès MongoDB pour les todos
│   └── TokenService.cs        # Génération des tokens JWT
├── Program.cs                 # Configuration de l'application
├── appsettings.json           # Paramètres (MongoDB, JWT)
├── Dockerfile                 # Image Docker de l'API
└── docker-compose.yml         # Orchestration API + MongoDB
```

---

## 🔗 Endpoints

| Méthode | Route | Accès |
|---------|-------|-------|
| POST | `/api/auth/register` | Public |
| POST | `/api/auth/login` | Public |
| GET | `/api/todos` | Connecté |
| GET | `/api/todos/{id}` | Connecté |
| POST | `/api/todos` | Admin |
| PUT | `/api/todos/{id}` | Admin |
| DELETE | `/api/todos/{id}` | Admin |

---

## 🛠 Technologies

- ASP.NET Core 8
- MongoDB 7
- ASP.NET Core Identity (MongoDbCore)
- JWT Bearer Authentication
- Swagger / OpenAPI
- Docker & Docker Compose
