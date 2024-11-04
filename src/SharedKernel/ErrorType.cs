namespace SharedKernel;

/// <summary>Error type.</summary>
public enum ErrorType
{
	/// <summary>Error failure.</summary>
	Failure = 0,

	/// <summary>Error validation.</summary>
	Validation = 1,

	/// <summary>Error problem.</summary>
	Problem = 2,

	/// <summary>Error Not found.</summary>
	NotFound = 3,

	/// <summary>Error conflict.</summary>
	Conflict = 4
}