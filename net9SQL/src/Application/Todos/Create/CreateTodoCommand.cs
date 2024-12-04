namespace Application.Todos.Create;

/// <summary>Create a to do command.</summary>
/// <param name="UserId">User Identifier.</param>
/// <param name="Description">To Do description.</param>
/// <param name="Priority">Priority.</param>
/// <param name="Labels">Labels.</param>
/// <param name="DueDate">Due date and time.</param>
public sealed record CreateTodoCommand(Guid UserId, string Description, Priority Priority, ReadOnlyCollection<string> Labels, DateTime? DueDate) : ICommand<Guid>;