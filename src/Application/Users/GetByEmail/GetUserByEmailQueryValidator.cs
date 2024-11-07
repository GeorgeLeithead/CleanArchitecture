namespace Application.Users.GetByEmail;

/// <summary>Get a user by email address query validation rules.</summary>
public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
	/// <summary>Initializes a new instance of the <see cref="GetUserByEmailQueryValidator" /> class.</summary>
	public GetUserByEmailQueryValidator() => _ = RuleFor(c => c.Email)
			.NotEmpty().WithMessage("Email address is required.")
			.EmailAddress().WithMessage("Please specify a valid email address.")
			.MinimumLength(3).WithMessage("Email Address must be at least 3 characters long.")
			.MaximumLength(320).WithMessage("Email Addresses cannot exceed 320 characters.");
}