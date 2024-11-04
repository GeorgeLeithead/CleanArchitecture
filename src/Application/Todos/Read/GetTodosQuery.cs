namespace Application.Todos.Read;

/// <summary>Get to do's by query.</summary>
/// <param name="UserId">A <see cref="Guid"/> user identifier.</param>
public sealed record GetTodosQuery(Guid UserId) : IQuery<List<TodoResponse>>;
