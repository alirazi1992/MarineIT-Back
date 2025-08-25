# ⚙️ MarineIT Backend (ASP.NET Core Web API)

This repository contains the **backend API** for the Marine ITSM system.  
It exposes RESTful endpoints for **Tickets, Vessels, Assets, and Maintenance tasks**, using **Entity Framework Core** with SQL Server.

---

## 🚀 Features
- 🎫 Ticket Management (CRUD)
- ⚓ Vessel Management
- 🛠 Asset & Maintenance Management
- 🔐 Identity + JWT Authentication
- 🌍 Swagger/OpenAPI documentation

---

## 🛠️ Tech Stack
- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server** (LocalDB or production)
- **JWT Authentication**
- **Swagger UI** for API exploration

---

## 📂 Project Structure

MarineIT-Back/

├── MarineIT.Api/ # API entry (controllers, startup)

├── MarineIT.Application/ # business logic

├── MarineIT.Domain/ # entities, enums

├── MarineIT.Infrastructure/ # EF Core, DbContext, repositories

└── Properties/

---

## ▶️ Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/alirazi1992/MarineIT-Back.git
   cd MarineIT-Back/MarineIT.Api
   ```
2. Restore dependencies:
```bash
dotnet restore
```
3. Apply migrations and update the database:
```bash
dotnet ef database update
```
4. Run the backend:
```bash
   dotnet run
```
5. API will be available at:
```bash
https://localhost:7150/swagger
```
----

## 🔗 API Endpoints

Example endpoints:

- `GET /api/Tickets`

- `POST /api/Tickets`

- `GET /api/Vessels`

- `POST /api/Vessels`

 --- 
 ## 🤝 Contributing 

Feel free to fork the repo and submit PRs or raise issues for any suggastions.


## 📬  Contact
For questions or collaboration opportunities:

**📧 Email:** ali.razi9292@gmail.com

**🔗 LinkedIn:** linkedin.com/in/alirazi1992


   
