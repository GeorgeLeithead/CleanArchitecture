namespace Application.Users.Register;

/// <summary>Register user command.</summary>
/// <param name="Email">Email address</param>
/// <param name="Password">Password.</param>
/// <param name="ConfirmPassword">Confirm password.</param>
public sealed record RegisterUserCommand(string Email, string Password, string ConfirmPassword) : ICommand<Guid>;