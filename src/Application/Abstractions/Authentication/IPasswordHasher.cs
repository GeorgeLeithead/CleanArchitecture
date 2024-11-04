namespace Application.Abstractions.Authentication;

/// <summary>Password hasher interface.</summary>
public interface IPasswordHasher
{
	/// <summary>Hash.</summary>
	/// <param name="password">Password.</param>
	/// <returns>A hashed password.</returns>
	string Hash(string password);

	/// <summary>Verify.</summary>
	/// <param name="password">Password.</param>
	/// <param name="passwordHash">Password hash.</param>
	/// <returns>A verified password using the supplied hash.</returns>
	bool Verify(string password, string passwordHash);
}