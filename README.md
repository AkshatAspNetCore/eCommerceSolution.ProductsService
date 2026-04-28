# Products Microservice — eCommerce Solution

Part of a **3-service microservices architecture** built with **ASP.NET Core**, demonstrating clean architecture and resilient inter-service communication.

## 🏗️ Architecture Overview

This Products Microservice is one of three services in a distributed eCommerce system:

| Service | Responsibility | Database |
|---------|----------------|----------|
| UsersService | User management *(JWT & registration coming soon)* | PostgreSQL |
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

**Frontend** *(in development)*
- Angular — UI for orders, products, users

## 📐 Project Structure (Clean Architecture)
├── BusinessLogicLayer/              # Services, DTOs, Mappers, Validators  
├── DataAccessLayer/                 # Repositories, Entities, DbContext  
├── ECommerceSolution.ProductsService/  # API layer, Controllers, Program.cs, Dockerfile  
└── ECommerceSolution.ProductsService.sln  

## 🚀 Getting Started

Detailed setup instructions and prerequisites will follow soon. 🛠️

Project is actively under development — stay tuned.

## ✨ Key Features

- ✅ **Minimal APIs** — lightweight and high-performance endpoints
- ✅ **Clean Architecture** — strict separation of concerns across layers
- ✅ **Repository Pattern** with Expression-based filtering
- ✅ **Async/await** throughout the codebase
- ✅ **AutoMapper profiles** for DTO mappings
- ✅ **FluentValidation** for request validation
- ✅ **Containerized** with Docker for consistent deployment

## 🔮 Roadmap

- [ ] Search and filtering endpoints
- [ ] Pagination support
- [ ] Unit tests (xUnit + Moq)
- [ ] RabbitMQ integration for product events
- [ ] Angular frontend (in development)
- [ ] Azure deployment

## Learning Project

Built while working through ".NET Microservices with Azure DevOps & AKS | Basic to Master"(https://www.udemy.com/course/dot-net-microservices-ecommerce-project-azure-devops-kubernetes-aks/learn/lecture/45853823?start=1#overview) by Harsha Vardhan on Udemy.

## 👨‍💻 Author

**Akshat Parasher** — Software Engineer | C#/.NET Developer | Germany 🇩🇪

- 🔗 [Portfolio](https://akshat95-portfolio.netlify.app)
- 🔗 [GitHub](https://github.com/AkshatAspNetCore)
- 🔗 [GitLab](https://gitlab.com/arkhamknight95-group)
