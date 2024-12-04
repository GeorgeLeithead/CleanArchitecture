// Ignore Spelling: authorised

namespace Domain.Users;

/// <summary>User Errors.</summary>
public static class UserErrors
{
	/// <summary>Not Found error.</summary>
	/// <param name="userId">A user <see cref="User.Id"/>.</param>
	/// <returns>An <see cref="Error.NotFound(string, string)"/>.</returns>
	public static Error NotFound(Guid userId) => Error.NotFound("Users.NotFound", $"The user with the Id = '{userId}' was not found.");

	/// <summary>Not authorised error.</summary>
	/// <returns>An <see cref="Error.Failure(string, string)"/>.</returns>
	public static Error Unauthorized() => Error.Failure("Users.Unauthorised", "You are not authorised to perform this action.");

	/// <summary>Not found by email error.</summary>
	public static readonly Error NotFoundByEmail = Error.NotFound("Users.NotFoundByEmail", "The user with the specified email was not found.");

	/// <summary>Email address not unique error.</summary>
	public static readonly Error EmailNotUnique = Error.Conflict("Users.EmailNotUnique", "The provided email is not unique.");
}