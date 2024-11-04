namespace SharedKernel;

/// <summary>Validation error class.</summary>
public sealed record ValidationError : Error
{
	/// <summary>Initializes a new instance of the <see cref="ValidationError" /> class.</summary>
	/// <param name="errors">An array of <see cref="Error"/>.</param>
	public ValidationError(Error[] errors) : base("Validation.General", "One or more validation errors occurred", ErrorType.Validation) => this.errors = errors;

	readonly Error[] errors;

	/// <summary>Validation errors.</summary>
	public Error[] GetErrors() => errors;

	/// <summary>Validation error results.</summary>
	/// <param name="results">An <see cref="IEnumerable{T}"/> of results.</param>
	/// <returns>A <see cref="ValidationError"/> of failure results.</returns>
	public static ValidationError FromResults(IEnumerable<Result> results) => new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}