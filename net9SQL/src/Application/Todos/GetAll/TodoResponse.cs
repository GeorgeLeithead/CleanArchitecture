namespace Application.Todos.GetAll;

/// <summary>To do response.</summary>
public sealed class TodoResponse
{
	/// <summary>Completed date and time.</summary>
	public DateTime? CompletedAt { get; set; }

	/// <summary>Created date and time.</summary>
	public required DateTime CreatedAt { get; set; }

	/// <summary>Description.</summary>
	public required string Description { get; set; }

	/// <summary>Due date and time.</summary>
	public DateTime? DueDate { get; set; }

	/// <summary>Response unique identifier.</summary>
	public Guid Id { get; set; }

	/// <summary>Completed indicator.</summary>
	public bool IsCompleted { get; set; }

	/// <summary>Labels.</summary>
	public required ReadOnlyCollection<string> Labels { get; set; }

	/// <summary>User identifier.</summary>
	public required Guid UserId { get; set; }
}