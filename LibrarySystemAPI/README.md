# Library System API

A simple RESTful API for managing a library system, including books, users, and loans.

This project was built as part of my backend development learning journey using ASP.NET Core.

## Features

- Manage books (create, read, update, delete)
- Manage users
- Loan system:
  - Borrow books
  - Return books
  - Prevent double loans
  - Prevent deleting users/books with active loans

## Technologies

- C#
- ASP.NET Core Web API
- In-memory data storage (DataStore)

## Endpoints

### Books
- `GET /books`
- `GET /books/{id}`
- `POST /books`
- `PUT /books/{id}`
- `DELETE /books/{id}`

### Users
- `GET /users`
- `GET /users/{id}`
- `POST /users`
- `PUT /users/{id}`
- `DELETE /users/{id}`

### Loans
- `GET /loans`
- `GET /loans/{id}`
- `POST /loans`
- `POST /loans/return/{id}`

## Business Rules

- A book cannot be loaned if it is already loaned
- A user cannot be deleted if they have active loans
- A book cannot be deleted if it is currently loaned
- A loan cannot be returned twice

## Notes

- This project uses in-memory storage (no database yet)
- Future improvements will include database integration and better architecture

## Project Evolution

This repository will be continuously updated as I learn more about backend development, including:

- Dependency Injection
- Entity Framework
- DTOs
- Authentication & Authorization

---

## Author

Developed as part of my learning journey.