namespace Application.Abstractions.Authentication;

/// <summary>Token provider interface.</summary>
public interface ITokenProvider
{
	/// <summary>Create.</summary>
	/// <param name="user">A <see cref="User"/>.</param>
	/// <returns>A token for the supplied user.</returns>
	string Create(User user);
}