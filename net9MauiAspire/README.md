# Clean Architecture - MAUI with Aspire

## Overview
Based on the template by [Milan Jovanović](https://twitter.com/mjovanovictech), see [Master The Clean Architecture](https://www.milanjovanovic.tech/pragmatic-clean-architecture) or get the base clean-architecture template here [Free Clean Architecture Template](https://www.milanjovanovic.tech/templates/clean-architecture).

Clean Architecture is a software design pattern that helps organize and structure applications in a way that separates business logic from implementation details. This approach ensures that the core functionality of the application remains independent of external factors like frameworks, databases, or user interfaces.

### Key Components of Clean Architecture:
1. **Entities**:
   - Represent the core business objects within the application.
   - Contain the most fundamental business rules and are independent of any external systems.
2. **Use Cases**:
   - Define the application-specific business rules.
   - Contain the logic for specific operations and interact with entities to execute business processes.
   - Ensure that the application behaves correctly and meets the requirements.
3. **Interface Adapters**:
   - Act as a bridge between the use cases and the external world.
   - Convert data from the format most convenient for the use cases and entities to the format required by the external systems (e.g., UI, database).
   - Include controllers, presenters, and gateways.
4. **Infrastructure**:
   - Represents the outermost layer of the architecture.
   - Contains implementation details such as frameworks, databases, and external services.
   - Provides the necessary tools and technologies to support the application but does not contain business logic.

### Benefits of Clean Architecture:
- **Maintainability**: By keeping business logic separate from implementation details, the codebase is easier to understand and modify.
- **Testability**: Business logic can be tested independently of external systems, leading to more reliable and faster tests.
- **Scalability**: The modular structure allows for easy addition of new features and components without affecting existing functionality.
- **Flexibility**: The architecture can adapt to changes in technology or requirements without significant rewrites.

### Best Practices for Implementing Clean Architecture in C#:
- **Decoupling**: Ensure that the core of the application (entities and use cases) is free of dependencies on external systems.
- **SOLID Principles**: Apply the SOLID principles to create a flexible and extensible codebase.
- **Asynchronous Operations**: Implement use cases asynchronously to improve scalability and performance.
- **Layered Approach**: Clearly define and separate the responsibilities of each layer to maintain a clean and organized structure.

By following these principles and practices, Clean Architecture helps create robust, scalable, and maintainable software applications.

## What's included in this repository?
- Using .NET9
- Aspire
- MAUI Hybrid UI
- SharedKernel project with:
  - Domain-Driven Design abstractions, including:
    - Entity
    - Error & ErrorType
    - Result matching
    - Validation
- Domain layer with:
  - sample entities
- Application layer with abstractions for:
  - CQRS, see [CQRS Pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs) and [Apply simplified CQRS and DDD patterns in a microservice](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/apply-simplified-microservice-cqrs-ddd-patterns).
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
- Docker Support
  - MicroService architecture, see [.net Microservices](https://dotnet.microsoft.com/download/e-book/microservices-architecture/pdf)
- Testing projects
  - Architecture testing