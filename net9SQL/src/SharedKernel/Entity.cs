namespace SharedKernel;

/// <summary>Entity class.</summary>
public abstract class Entity
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