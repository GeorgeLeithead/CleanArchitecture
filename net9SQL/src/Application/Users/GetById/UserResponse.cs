namespace Application.Users.GetById;

/// <summary>User response.</summary>
public sealed record UserResponse
{
	/// <summary>Unique identifier.</summary>
	public required Guid Id { get; init; }

	/// <summary>Email address.</summary>
	public required string Email { get; init; }

	/// <summary>First Name.</summary>
	public required string FirstName { get; init; }

	/// <summary>Last Name.</summary>
	public required string LastName { get; init; }
}