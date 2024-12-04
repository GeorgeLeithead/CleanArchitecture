namespace Application.Users.GetByEmail;

/// <summary>Get user by email query.</summary>
/// <param name="Email">User Email address.</param>
public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;