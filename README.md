# Clean Architecture Template

## Overview
Based on the template by Milan Jovanović.

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
