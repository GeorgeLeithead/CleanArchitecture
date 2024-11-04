# Clean Architecture Template

## Overview
Based on the template by [Milan Jovanović](https://twitter.com/mjovanovictech), see [Master The Clean Architecture](https://www.milanjovanovic.tech/pragmatic-clean-architecture) or get the base clean-architecture template here [Free Clean Architecture Template](https://www.milanjovanovic.tech/templates/clean-architecture).

## What's included
- Using .NET9
- SharedKernel project with common Domain-Driven Design abstractions.
- Domain layer
  - with sample entities.
- Application layer with abstractions for:
  - CQRS
  - Cross-cutting concerns
    - logging
    - validation
- Infrastructure layer with:
  - Authentication (JWT)
  - Permission authorization
  - EF Core, SQL Server
  - Serilog
- Presentation Layer
  - API Versioning
  - API Documentation
    - Removal of Swagger/Swashbuckle
    - OpenAPI - Documentation
    - Scalar - Interactive documentation
- Testing projects
  - Architecture testing

## TODO
- Remove Swagger and replace with OpenApi
- Add YARP
- Add Rate limiting
- Add Resilience to the DB
- Add multi-tenancy, by using a Discriminator, see: [Supporting multi-tenancy](https://learn.microsoft.com/en-us/ef/core/miscellaneous/multitenancy) and [Multi-Tenant Applications With EF Core](https://www.milanjovanovic.tech/blog/multi-tenant-applications-with-ef-core)
- Add WebSocket's support, see [WebSockets support in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-9.0)
