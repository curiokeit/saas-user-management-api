# 🚀 SaaS User Management & Task Management API

A multi-tenant SaaS backend system built with ASP.NET Core Web API.

---

## 🔥 Features

- Multi-tenant architecture (company-based data isolation)
- JWT Authentication & Role-Based Authorization
- User management (Admin / User roles)
- Subscription plan system (Free / Pro / Business)
- User limit control based on plan
- Task assignment and tracking system
- Task status updates (Todo / InProgress / Done)
- RESTful API design
- Swagger API documentation
- Full Postman API testing collection

---

## 🛠 Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (OpenAPI)
- Postman

---

## 📂 Project Structure

SaasUserManagement.API/
postman/
README.md

---

## ▶️ How to Run

1. Run database migrations:
dotnet ef database update

2. Run the project:
dotnet run

3. Open Swagger:
https://localhost:{PORT}/swagger

---

## 🔐 Authentication

This API uses JWT authentication.

Login endpoint:
POST /api/Auth/login

After login:
- Token is returned
- Use it in requests:

Authorization: Bearer {token}

---

## 📡 API Endpoints

Users:
- GET /api/Users
- POST /api/Users

Tasks:
- GET /api/Tasks
- GET /api/Tasks/my-tasks
- POST /api/Tasks
- PUT /api/Tasks/{id}/status

Dashboard:
- GET /api/Dashboard/summary

Plans:
- GET /api/Plans
- PUT /api/Plans/upgrade

---

## 🚀 API Testing (Postman)

You can test the API using the included Postman collection:

postman/SaaS API.postman_collection.json

Steps:

1. Import collection into Postman
2. Set base_url = https://localhost:{PORT}
3. Run Login request
4. Token will be auto-saved
5. Test all endpoints

---

## 💡 Highlights

- Built a real-world SaaS backend system
- Implemented secure JWT authentication
- Designed RESTful APIs
- Created full Postman testing setup
- Multi-tenant architecture

---

## 👨‍💻 Author

Recep Emre Odemis  
Istanbul, Turkey  
Software Developer