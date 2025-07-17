# 🧠 GuiDevConnect – Backend (.NET)

**GuiDevConnect Backend** est l’API REST qui alimente la plateforme communautaire _GuiDevConnect_, un espace dédié aux développeurs guinéens pour échanger, apprendre, partager des opportunités et renforcer l'écosystème tech local.

---

![.NET](https://img.shields.io/badge/.NET-7.0-blue?style=flat-square&logo=dotnet)
![API REST](https://img.shields.io/badge/API-RESTful-green?style=flat-square)
![Made in Guinea](https://img.shields.io/badge/Made%20with-%E2%9D%A4%20in%20Guinea-red?style=flat-square)
[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC--BY--NC--ND%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

---

## 🚀 Fonctionnalités principales

- 🧑‍💻 Authentification sécurisée (JWT)
- 📬 Gestion des utilisateurs, rôles, profils et communautés
- 🧵 Forums, discussions, commentaires
- 📦 Architecture modulaire et scalable en .NET

---

## ⚙️ Stack technique

| Élément           | Techno                            |
|------------------|-----------------------------------|
| Langage          | C# ( .NET 8)                      |
| Framework        | ASP.NET Core Web API              |
| Authentification | JWT Bearer Token                  |
| Base de données  |  MongoDB                          |
| ORM              | Entity Framework Core             |
| Documentation    | Swagger                           |
| Cache            | Redis (optionnel)                 |
| Messaging        | SignalR (temps réel)              |  

---

## 🛠️ Installation

```bash
# 1. Cloner le repo
git clone https://github.com/Khalil-keita/GuiDevConnect-Backend.git
cd GuiDevConnect-Backend

# 2. Restaurer les packages
dotnet restore

# 3. Lancer le projet
dotnet run
