namespace Application.Users.Register;

/// <summary>User Registration validation rules.</summary>
sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		_ = RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(255);
		_ = RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(255);
		_ = RuleFor(c => c.Email).NotEmpty().EmailAddress().MinimumLength(3).MaximumLength(320);
		_ = RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
	}
}