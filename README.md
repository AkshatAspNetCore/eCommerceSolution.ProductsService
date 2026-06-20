# Products Microservice — eCommerce Solution

Part of a **3-service microservices architecture** built with **ASP.NET Core**, demonstrating clean architecture and resilient inter-service communication.

## 📝 Note for Reviewers

[!IMPORTANT]
> This is a **cost-optimized learning deployment**. The databases run as **ephemeral pods with no persistent storage attached**, and the AKS cluster is shut down outside of demo hours to keep Azure costs near zero.

**What this means in practice:**
- **Data does not persist.** Products, users, and orders you create live only inside the pod's container filesystem.
- **State resets** whenever the cluster is turned off, or when pods are recreated (which happens on every restart — effectively a daily reset).
- Each pod comes back **fresh and empty**.
- If you revisit the app the next day, expect a **clean slate** — this is intentional, not a bug.

In a production setup, durability would come from **managed databases** (e.g., Azure Database for MySQL, Azure Cosmos DB) or **PersistentVolumes / StatefulSets** backed by Azure Disks, so data would survive restarts. That's deliberately left out here to keep the project free to host while still demonstrating the full CI/CD → AKS pipeline.

## 🏗️ Architecture Overview

This Products Microservice is one of three services in a distributed eCommerce system:

![eCommerce microservices architecture](products_microservice_architecture_v3.png)

| Service | Responsibility | Database |
|---------|----------------|----------|
| UsersService | User management *(JWT & registration)* | Entra ID |
| **ProductsService** *(this repo)* | Product catalog and inventory | MySQL |
| OrdersService | Order processing and validation | MongoDB |

Other services consume this microservice via **HTTP clients** with **Polly-based fault tolerance**.

## 🛠️ Tech Stack

**Backend**
- ASP.NET Core — Minimal APIs
- Entity Framework Core — MySQL provider
- AutoMapper — DTO ↔ Entity mapping
- FluentValidation — input validation
- Repository Pattern — clean separation between data and business logic

**Infrastructure**
- Docker & Docker Compose
- MySQL — relational database
- Integrated with Ocelot API Gateway (from OrdersService repo)

**Frontend** 
- Angular — UI for orders, products, users

## 📐 Project Structure (Clean Architecture)
├── BusinessLogicLayer/              # Services, DTOs, Mappers, Validators  
├── DataAccessLayer/                 # Repositories, Entities, DbContext  
├── ECommerceSolution.ProductsService/  # API layer, Controllers, Program.cs, Dockerfile  
└── ECommerceSolution.ProductsService.sln  

## ✨ Key Features

- ✅ **Minimal APIs** — lightweight and high-performance endpoints
- ✅ **Clean Architecture** — strict separation of concerns across layers
- ✅ **Repository Pattern** with Expression-based filtering
- ✅ **Async/await** throughout the codebase
- ✅ **AutoMapper profiles** for DTO mappings
- ✅ **FluentValidation** for request validation
- ✅ **Containerized** with Docker for consistent deployment


## Learning Project

Built while working through ".NET Microservices with Azure DevOps & AKS | Basic to Master"(https://www.udemy.com/course/dot-net-microservices-ecommerce-project-azure-devops-kubernetes-aks/learn/lecture/45853823?start=1#overview) by Harsha Vardhan on Udemy.

## 👨‍💻 Author

**Akshat Parasher** — Software Engineer | C#/.NET Developer | Germany 🇩🇪

- 🔗 [Portfolio](https://akshat95-portfolio.netlify.app)
- 🔗 [GitHub](https://github.com/AkshatAspNetCore)
- 🔗 [GitLab](https://gitlab.com/arkhamknight95-group)
