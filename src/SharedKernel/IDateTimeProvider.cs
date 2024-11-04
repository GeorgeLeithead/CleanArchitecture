namespace SharedKernel;

/// <summary>Date Time provider interface.</summary>
public interface IDateTimeProvider
{
	/// <summary>UTC Now.</summary>
	public DateTime UtcNow { get; }
}