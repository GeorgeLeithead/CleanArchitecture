namespace Domain.Todos;

/// <summary>To do item created domain event.</summary>
/// <param name="TodoItemId">A <see cref="TodoItem.Id"/>.</param>
public sealed record TodoItemCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;