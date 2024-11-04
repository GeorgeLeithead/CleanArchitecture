namespace Application.Abstractions.Authentication;

/// <summary>User context interface.</summary>
public interface IUserContext
{
	/// <summary>A user <see cref="Guid" />.</summary>
	Guid UserId { get; }
}