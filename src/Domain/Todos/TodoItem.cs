namespace Domain.Todos;

/// <summary>TodoItem.</summary>
public sealed class TodoItem : Entity
{
	/// <summary>Unique identifier.</summary>
	public Guid Id { get; set; }

	/// <summary>User unique identifier.</summary>
	public required Guid UserId { get; set; }

	/// <summary>Description.</summary>
	public required string Description { get; set; }

	/// <summary>Due date.</summary>
	public DateTime? DueDate { get; set; }

	/// <summary>Labels.</summary>
	public required ReadOnlyCollection<string> Labels { get; set; }

	/// <summary>Completed flag.</summary>
	public bool IsCompleted { get; set; }

	/// <summary>Created date and time.</summary>
	public required DateTime CreatedAt { get; set; }

	/// <summary>Completed date and time.</summary>
	public DateTime? CompletedAt { get; set; }

	/// <summary>Priority</summary>
	public required Priority Priority { get; set; }
}