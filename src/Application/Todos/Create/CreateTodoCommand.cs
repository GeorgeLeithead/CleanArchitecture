namespace Application.Todos.Create;

/// <summary>Create a to do command.</summary>
public sealed class CreateTodoCommand : ICommand<Guid>
{
	/// <summary>Description.</summary>
	public required string Description { get; set; }

	/// <summary>Due date.</summary>
	public DateTime? DueDate { get; set; }

	/// <summary>Labels.</summary>
#pragma warning disable CA1002 // Do not expose generic lists
#pragma warning disable CA2227 // Collection properties should be read only
	public List<string> Labels { get; set; } = [];
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1002 // Do not expose generic lists

	/// <summary>Priority.</summary>
	public required Priority Priority { get; set; }

	/// <summary>User identifier.</summary>
	public required Guid UserId { get; set; }
}
