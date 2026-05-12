# Library System API

A RESTful API for managing a library system, including books, users, and loans.

This project was built as part of my backend development learning journey using ASP.NET Core and Entity Framework Core.

---

# Features

- Manage books (create, read, update, delete)
- Manage users
- Loan system:
  - Borrow books
  - Return books
  - Prevent double loans
  - Prevent deleting users/books with active loans

---

# Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger / OpenAPI
- Dependency Injection

---

# Architecture

The project currently follows a layered structure:

```text
Controllers
↓
Services
↓
Entity Framework Core
↓
MySQL
Endpoints
Books
GET /books
GET /books/{id}
POST /books
PUT /books/{id}
DELETE /books/{id}
Users
GET /users
GET /users/{id}
POST /users
PUT /users/{id}
DELETE /users/{id}
Loans
GET /loans
GET /loans/{id}
POST /loans/loan
POST /loans/return/{id}
Business Rules
A book cannot be loaned if it is already loaned
A user cannot be deleted if they have active loans
A book cannot be deleted if it is currently loaned
A loan cannot be returned twice

# Database Setup

1. Create the database
CREATE DATABASE library_system;

2. Configure Connection String

Create your own appsettings.json file based on:

appsettings.example.json

Example:

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=library_system;user=root;password=YOUR_PASSWORD"
  }
}

3. Apply Migrations

Run:

dotnet ef database update
Running the Project
dotnet run

# Swagger Documentation

After running the project, open:

https://localhost:xxxx/swagger

Swagger provides:

Interactive API testing
Endpoint documentation
Request/response examples
Project Evolution

This repository will continue evolving as I learn more about backend development.

# Future improvements include:

DTOs
Async/Await
Authentication & Authorization (JWT)
Better validations
Repository Pattern
Frontend integration
Docker
Deployment

# Notes

This project started using in-memory storage and was later migrated to MySQL using Entity Framework Core.

The goal of this repository is to document my backend learning journey and progressively improve the architecture over time.

# Author

Murillo Miranda Santos