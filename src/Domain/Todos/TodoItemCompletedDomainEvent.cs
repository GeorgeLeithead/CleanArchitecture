namespace Domain.Todos;

/// <summary>To do item completed domain event.</summary>
/// <param name="TodoItemId">A <see cref="TodoItem.Id"/>.</param>
public sealed record TodoItemCompletedDomainEvent(Guid TodoItemId) : IDomainEvent;