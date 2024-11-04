namespace Application.Todos.GetById;

/// <summary>Get to do's by query using unique identifier.</summary>
/// <param name="TodoItemId">A <see cref="Guid"/> to do unique identifier.</param>
public sealed record GetTodoByIdQuery(Guid TodoItemId) : IQuery<TodoResponse>;