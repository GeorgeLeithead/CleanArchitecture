namespace Application.Abstractions.Data;

/// <summary>Application DB context.</summary>
public interface IApplicationDbContext
{
	/// <summary>Save changes.</summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A <see cref="int" /> of how many items have been saved.</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}