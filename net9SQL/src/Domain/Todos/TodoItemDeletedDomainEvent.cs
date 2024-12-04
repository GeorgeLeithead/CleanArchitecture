namespace Domain.Todos;

/// <summary>To do item deleted domain event.</summary>
/// <param name="TodoItemId">A <see cref="TodoItem.Id"/>.</param>
public sealed record TodoItemDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;