namespace Application.Todos.Delete;

/// <summary>Delete a to do command.</summary>
/// <param name="TodoItemId">A <see cref="Guid"/> to-do item.</param>
public sealed record DeleteTodoCommand(Guid TodoItemId) : ICommand;