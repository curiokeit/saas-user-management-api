# SaaS User Management & Task Management API

A multi-tenant SaaS backend project built with ASP.NET Core Web API.

## Features

- JWT Authentication
- Role-Based Authorization
- Multi-Tenant Company Structure
- User Management
- Subscription Plan Logic
- User Limit Control
- Task Assignment
- Task Status Updates
- Swagger API Documentation

## Technologies

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- JWT
- Swagger

## Roles

### Admin
- Create users
- View company users
- Create tasks
- Assign tasks
- Upgrade subscription plan

### User
- View assigned tasks
- Update task status

## Subscription Plans

| Plan | User Limit |
|---|---:|
| Free | 3 |
| Pro | 10 |
| Business | 50 |

## API Endpoints

### Auth

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new company/admin |
| POST | `/api/Auth/login` | Login and get JWT token |

### Users

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Users` | Get company users |
| POST | `/api/Users` | Create new user |

### Plans

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Plans` | Get subscription plans |
| PUT | `/api/Plans/upgrade` | Upgrade company plan |

### Dashboard

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Dashboard/summary` | Get company dashboard summary |

### Tasks

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Tasks` | Get tasks |
| GET | `/api/Tasks/my-tasks` | Get assigned tasks |
| POST | `/api/Tasks` | Create task |
| PUT | `/api/Tasks/{id}/status` | Update task status |

## How to Run

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run migrations:

```bash
dotnet ef database update