# 🚀 SaaS User Management & Task Management API

A multi-tenant SaaS backend project built with ASP.NET Core Web API.

## 🔥 Features

- Multi-tenant architecture (company-based data isolation)
- JWT Authentication & Role-Based Authorization
- User management with Admin/User roles
- Subscription plan system (Free / Pro / Business)
- User limit control based on plan
- Task assignment and tracking system
- Task status updates (Todo / InProgress / Done)
- Swagger API documentation

## 🧱 Technologies

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger

## 👥 Roles

### Admin
- Create users
- Assign tasks
- Upgrade subscription plan
- View all company data

### User
- View assigned tasks
- Update task status

## 💳 Subscription Plans

| Plan | User Limit |
|------|------------|
| Free | 3 |
| Pro | 10 |
| Business | 50 |

## 📌 API Endpoints

### Auth
- POST /api/Auth/register
- POST /api/Auth/login

### Users
- GET /api/Users
- POST /api/Users

### Plans
- GET /api/Plans
- PUT /api/Plans/upgrade

### Dashboard
- GET /api/Dashboard/summary

### Tasks
- GET /api/Tasks
- GET /api/Tasks/my-tasks
- POST /api/Tasks
- PUT /api/Tasks/{id}/status

## ▶️ How to Run

```bash
dotnet ef database update
dotnet run

Then open: https://localhost:xxxx/swagger
