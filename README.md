# Financial Overview

This repository contains an ASP.NET Core/C# backend, a minimal React + Vite/TypeScript frontend, and a `docker-compose.yml` file for local development with PostgreSQL.

 Quick Overview

- Backend: `WebApplication1` — C#, .NET 10, and Entity Framework Core.
- Frontend: `frontend` — React, Vite, and TypeScript.
- Database: PostgreSQL, running through Docker Compose.

## Security Notes

- Do not commit secrets. Store them in an untracked `.env` file, .NET User Secrets, or a secrets manager.
- Do not use `ASPNETCORE_ENVIRONMENT=Development` in production.
- Implement authentication and authorization before publishing the API.
- The global error-handling middleware returns generic messages to avoid exposing internal details.

## ⏳ In Progress — Roadmap

1. Authentication with JWT and ASP.NET Core Identity.
2. Full CRUD operations for transactions and categories.
3. A dashboard with charts.
4. Unit and integration tests.
5. CI/CD and Docker image publishing.

## Development Instructions

See [Instructions](Instructions.md) for keyboard shortcuts and useful commands.