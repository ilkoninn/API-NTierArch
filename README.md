# 🧱 API-NTierArch

A clean, modular, and maintainable **.NET Web API** project built with a **custom N-Tier Architecture** approach. This repository showcases how to structure scalable backend applications by splitting responsibilities into distinct layers such as `API`, `Business`, `Core`, `DAL`, and `Shared`.

---

## 🧠 Architecture Overview

The goal of this architecture is to keep each layer focused on a single responsibility:

src/ <br/>
│ <br/>
├── App.API → Presentation Layer (Controllers, Middleware, Swagger) <br/>
├── App.Business → Business Logic Layer (Services, Interfaces, DTOs) <br/>
├── App.Core → Domain Layer (Entities, Enums, Contracts) <br/>
├── App.DAL → Data Access Layer (EF Core, DbContext, Repositories) <br/>
├── App.Shared → Shared Layer (Response Models, Base Classes) <br/>
└── App.sln → Solution File <br/>


This structure ensures:

- 🧩 Separation of concerns  
- 🧪 Improved testability  
- 📦 Scalable and maintainable codebase

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 7 or 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or any EF Core-supported database)
- Visual Studio 2022+ or Visual Studio Code

### ⚙️ Setup Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/ilkoninn/API-NTierArch.git
   cd API-NTierArch/src
