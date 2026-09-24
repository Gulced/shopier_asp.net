# Commerce Platform

A layered ASP.NET Core commerce application covering user authentication, product management, and order processing.

## Overview

A layered ASP.NET Core commerce application covering user authentication, product management, and order processing. The description and capabilities in this document are limited to behavior that can be verified in the repository source.

## Key Features

- User registration and login
- JWT authentication and BCrypt password hashing
- Product CRUD
- Order and order-item processing
- Repository and service abstractions
- EF Core migrations

## Tech Stack

- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT
- BCrypt

## Architecture

Controllers delegate business logic to service interfaces and implementations, which use repository abstractions over Entity Framework Core.

## Project Structure

- `web_donem_sonu/Controllers/` — API endpoints
- `web_donem_sonu/Services/` — business logic
- `web_donem_sonu/Data/Repositories/` — data access abstractions
- `web_donem_sonu/Models/` — entities and DTOs
- `web_donem_sonu/Migrations/` — database migrations
- `WebUI/` — web interface project

## Getting Started

Run the commands appropriate to the project root:

```bash
dotnet restore web_donem_sonu.sln
dotnet run --project web_donem_sonu/WebApi.csproj
```

## Environment Variables

Configure these names through local environment/configuration files. Do not commit secret values.

```env
ConnectionStrings__DefaultConnection=
Jwt__Key=
```

## Technical Highlights

- Layered controller/service/repository design
- JWT and BCrypt security flow
- Relational order and order-item model
- PostgreSQL migrations

## Possible Improvements

- Add or expand automated tests around core workflows.
- Document deployment and environment-specific configuration.
- Add CI checks for build, linting, and tests where they are not already present.
