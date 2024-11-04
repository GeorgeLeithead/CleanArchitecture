namespace Application.Users.GetById;

/// <summary>Get users by unique identifier.</summary>
/// <param name="UserId">A <see cref="Guid"/> unique user identifier.</param>
public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;