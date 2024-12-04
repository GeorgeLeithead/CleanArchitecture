namespace Domain.Todos;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>TodoItem.</summary>
[Table("todo_items")]
public sealed class TodoItem : Entity
{
	/// <summary>Unique identifier.</summary>
	public Guid Id { get; set; }

	/// <summary>User unique identifier.</summary>
	[Column("user_id")]
	public required Guid UserId { get; set; }

	/// <summary>Description.</summary>
	[Column("description")]
	public required string Description { get; set; }

	/// <summary>Due date.</summary>
	[Column("due_date")]
	public DateTime? DueDate { get; set; }

	/// <summary>Labels.</summary>
	[Column("labels")]
	public required ReadOnlyCollection<string> Labels { get; set; }

	/// <summary>Completed flag.</summary>
	[Column("is_completed")]
	public bool IsCompleted { get; set; }

	/// <summary>Created date and time.</summary>
	[Column("created_at")]
	public required DateTime CreatedAt { get; set; }

	/// <summary>Completed date and time.</summary>
	[Column("completed_at")]
	public DateTime? CompletedAt { get; set; }

	/// <summary>Priority</summary>
	[Column("priority")]
	public required Priority Priority { get; set; }
}