namespace Application.Users.Login;

/// <summary>Login user command</summary>
/// <param name="Email">Email address.</param>
/// <param name="Password">Password.</param>
public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;