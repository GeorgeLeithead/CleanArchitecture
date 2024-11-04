namespace Domain.Users;

/// <summary>User entity.</summary>
public sealed class User : Entity
{
	/// <summary>User GUID.</summary>
	public Guid Id { get; set; }

	/// <summary>User email.</summary>
	public required string Email { get; set; }

	/// <summary>User First Name.</summary>
	public required string FirstName { get; set; }

	/// <summary>User Last Name.</summary>
	public required string LastName { get; set; }

	/// <summary>User Password HASH.</summary>
	public required string PasswordHash { get; set; }
}
