namespace SharedKernel;

/// <summary>Error class.</summary>
#pragma warning disable CA1716 // Identifiers should not match keywords
public record Error
#pragma warning restore CA1716 // Identifiers should not match keywords
{
	/// <summary>Error none.</summary>
	public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

	/// <summary>Error null value.</summary>
	public static readonly Error NullValue = new("General.Null", "Null value was provided", ErrorType.Failure);

	/// <summary>Initializes a new instance of the <see cref="Error" /> class.</summary>
	/// <param name="code">Code where the error occurred.</param>
	/// <param name="description">Description of the error.</param>
	/// <param name="type">Error type <see cref="ErrorType"/>.</param>
	public Error(string code, string description, ErrorType type)
	{
		Code = code;
		Description = description;
		Type = type;
	}

	/// <summary>Code where the error occurred.</summary>
	public string Code { get; }

	/// <summary>Description of the error.</summary>
	public string Description { get; }

	/// <summary>Error type <see cref="ErrorType"/>.</summary>
	public ErrorType Type { get; }

	/// <summary>Failure error.</summary>
	/// <param name="code">Code where the error occurred.</param>
	/// <param name="description">Description of the error.</param>
	/// <returns>An <see cref="Error"/> of type <see cref="ErrorType.Failure"/>.</returns>
	public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);

	/// <summary>Not found error.</summary>
	/// <param name="code">Code where the error occurred.</param>
	/// <param name="description">Description of the error.</param>
	/// <returns>An <see cref="Error"/> of type <see cref="ErrorType.NotFound"/>.</returns>
	public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);

	/// <summary>Problem error.</summary>
	/// <param name="code">Code where the error occurred.</param>
	/// <param name="description">Description of the error.</param>
	/// <returns>An <see cref="Error"/> of type <see cref="ErrorType.Problem"/>.</returns>
	public static Error Problem(string code, string description) => new(code, description, ErrorType.Problem);

	/// <summary>Conflict error.</summary>
	/// <param name="code">Code where the error occurred.</param>
	/// <param name="description">Description of the error.</param>
	/// <returns>An <see cref="Error"/> of type <see cref="ErrorType.Conflict"/>.</returns>
	public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
}