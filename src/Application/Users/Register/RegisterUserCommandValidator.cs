namespace Application.Users.Register;

sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		_ = RuleFor(c => c.FirstName).NotEmpty();
		_ = RuleFor(c => c.LastName).NotEmpty();
		_ = RuleFor(c => c.Email).NotEmpty().EmailAddress();
		_ = RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
	}
}
