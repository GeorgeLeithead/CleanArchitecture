namespace Domain.Users;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>User entity.</summary>
public sealed class User : Entity
{
	/// <summary>User GUID.</summary>
	public Guid Id { get; set; }

	/// <summary>User email.</summary>
	[Column("email")]
	public required string Email { get; set; }

	/// <summary>User First Name.</summary>
	[Column("first_name")]
	public required string FirstName { get; set; }

	/// <summary>User Last Name.</summary>
	[Column("last_name")]
	public required string LastName { get; set; }

	/// <summary>User Password HASH.</summary>
	[Column("password_hash")]
	public required string PasswordHash { get; set; }
}