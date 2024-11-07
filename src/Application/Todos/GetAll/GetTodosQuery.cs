namespace Application.Todos.GetAll;

/// <summary>Get to do's by query.</summary>
/// <param name="UserId">A <see cref="Guid"/> user identifier.</param>
/// <param name="page">The page number to retrieve. Must be greater then or equal to 1. Defaults to 1</param>
/// <param name="pageSize">The number of elements to return per page.  Must be greater than or equal to 1. Defaults to 10</param>
public sealed record GetTodosQuery(Guid UserId, int page = 1, int pageSize = 10) : IQuery<List<TodoResponse>>;