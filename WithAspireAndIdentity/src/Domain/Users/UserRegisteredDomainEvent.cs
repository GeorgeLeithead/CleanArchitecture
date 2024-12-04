namespace Domain.Users;

/// <summary>User registered, domain event.</summary>
/// <param name="UserId">A user <see cref="Guid"/>.</param>
public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;