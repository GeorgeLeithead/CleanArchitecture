namespace Domain.Users;

public class AppUser : IdentityUser
{
	List<IDomainEvent>? domainEvents;

	/// <summary>Domain events.</summary>
	public IReadOnlyCollection<IDomainEvent>? DomainEvents => domainEvents?.AsReadOnly();

	/// <summary>Add a domain event.</summary>
	/// <param name="eventItem">Event item.</param>
	public void AddDomainEvent(IDomainEvent eventItem)
	{
		domainEvents ??= [];
		domainEvents.Add(eventItem);
	}

	/// <summary>Remove a domain event.</summary>
	/// <param name="eventItem">Event item.</param>
	public void RemoveDomainEvent(IDomainEvent eventItem) => domainEvents?.Remove(eventItem);

	/// <summary>Clear all domain events.</summary>
	public void ClearDomainEvents() => domainEvents?.Clear();
}

/// <summary>This is the DTO used to exchange username and password details to the create user and token endpoints.</summary>
public class UserInfo
{
	[Required]
	public string Email { get; set; } = default!;

	[Required]
	public string Password { get; set; } = default!;
}

/// <summary>This is the DTO used to exchange username and password details to the create user and token endpoints.</summary>
public class ExternalUserInfo
{
	[Required]
	public string Username { get; set; } = default!;

	[Required]
	public string ProviderKey { get; set; } = default!;
}