namespace Application.Abstractions.Data;

/// <summary>Application DB context.</summary>
public interface IApplicationDbContext
{
	/// <summary>To do items.</summary>
	DbSet<TodoItem> TodoItems { get; }

	/// <summary>Users.</summary>
	DbSet<User> Users { get; }

	/// <summary>Save changes.</summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A <see cref="int" /> of how many items have been saved.</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}