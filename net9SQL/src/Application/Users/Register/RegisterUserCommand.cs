namespace Application.Users.Register;

/// <summary>Register user command.</summary>
/// <param name="Email">Email address</param>
/// <param name="FirstName">First Name.</param>
/// <param name="LastName">Last Name.</param>
/// <param name="Password">Password.</param>
public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password) : ICommand<Guid>;