namespace Application.Todos.Read;

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
#pragma warning disable CA1002 // Do not expose generic lists
#pragma warning disable CA2227 // Collection properties should be read only
	public required List<string> Labels { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1002 // Do not expose generic lists

	/// <summary>User identifier.</summary>
	public required Guid UserId { get; set; }
}
