namespace Application.Todos.Complete;

/// <summary>Complete to do command.</summary>
/// <param name="TodoItemId">A <see cref="TodoItem.Id"/>.</param>
public sealed record CompleteTodoCommand(Guid TodoItemId) : ICommand;