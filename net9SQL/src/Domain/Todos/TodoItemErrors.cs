namespace Domain.Todos;

/// <summary>To do item errors.</summary>
public static class TodoItemErrors
{
	/// <summary>Already completed error.</summary>
	/// <param name="todoItemId">A <see cref="TodoItem.Id"/>.</param>
	/// <returns>A <see cref="Error.Problem(string, string)"/> error.</returns>
	public static Error AlreadyCompleted(Guid todoItemId) => Error.Problem("TodoItems.AlreadyCompleted", $"The to do item with Id = '{todoItemId}' is already completed.");

	/// <summary>Not found error.</summary>
	/// <param name="todoItemId">A <see cref="TodoItem.Id"/>.</param>
	/// <returns>A <see cref="Error.NotFound(string, string)"/> error.</returns>
	public static Error NotFound(Guid todoItemId) => Error.NotFound("TodoItems.NotFound", $"The to-do item with the Id = '{todoItemId}' was not found");
}