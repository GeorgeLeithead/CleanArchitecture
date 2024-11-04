# Clean Architecture Template

## Overview
Based on the template by [Milan Jovanović](https://twitter.com/mjovanovictech), see [Master The Clean Architecture](https://www.milanjovanovic.tech/pragmatic-clean-architecture) or get the base clean-architecture template here [Free Clean Architecture Template](https://www.milanjovanovic.tech/templates/clean-architecture).

## What's included
- Using .NET9
- SharedKernel project with:
  - Domain-Driven Design abstractions, including:
    - Entity
    - Error & ErrorType
    - Result matching
    - Validation
- Domain layer with:
  - sample entities
- Application layer with abstractions for:
  - CQRS
  - Cross-cutting concerns
    - Logging
    - Validation
- Infrastructure layer with:
  - Authentication
    - Claims Principals
    - Password hashing
    - Token provider (JWT)
    - User Context
  - Authorization
    - Permission authorization
    - Policy provider
  - EF Core with SQL Server
    - Migrations
    - Entity type configuration
- Presentation Layer (API) with:
  - Endpoint discovery/mapping
  - Results pattern
    - Custom results with Problem Details
    - Global exception handler
  - API Versioning
  - API Documentation
    - [OpenAPI](https://learn.microsoft.com/en-us/openapi/openapi.net/overview) - Documentation
    - [Scalar](https://scalar.com/) - Interactive documentation
  - [Serilog](https://serilog.net/)
    - Structured logging
  - [Seq](https://datalust.co/seq) support
- Testing projects
  - Architecture testing

## TODO
This is a list of general topics that we aim to include/implement:
- Add YARP (Reverse Proxy)
- Add Rate limiting (by user authorization)
- Add Resilience to the DB
- Add multi-tenancy, by using a Discriminator, see: [Supporting multi-tenancy](https://learn.microsoft.com/en-us/ef/core/miscellaneous/multitenancy) and [Multi-Tenant Applications With EF Core](https://www.milanjovanovic.tech/blog/multi-tenant-applications-with-ef-core)
- Add WebSocket's support, see [WebSockets support in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-9.0)
- Integration into .net Aspire
