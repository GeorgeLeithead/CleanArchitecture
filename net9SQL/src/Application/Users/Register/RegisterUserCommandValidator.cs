﻿namespace Application.Users.Register;

/// <summary>User Registration validation rules.</summary>
sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		// First Name validators
		_ = RuleFor(c => c.FirstName)
			.NotEmpty().WithMessage("First Name cannot be empty.")
			.MinimumLength(2).WithMessage("First Name must be at least 2 characters long.")
			.MaximumLength(255).WithMessage("First name cannot exceed 255 characters.");

		// Last Name validators
		_ = RuleFor(c => c.LastName)
			.NotEmpty().WithMessage("Last Name cannot be empty.")
			.MinimumLength(2).WithMessage("Last Name must be at least 2 characters long.")
			.MaximumLength(255).WithMessage("Last Name cannot exceed 255 characters.");

		// Email validators
		_ = RuleFor(c => c.Email)
			.NotEmpty().WithMessage("Email Address cannot be empty.")
			.EmailAddress().WithMessage("Please specify a valid email address.")
			.MinimumLength(3).WithMessage("Email Address must be at least 3 characters long.")
			.MaximumLength(320).WithMessage("Email Addresses cannot exceed 320 characters.");

		// Password validator
		_ = RuleFor(c => c.Password)
			.NotEmpty().WithMessage("Password cannot be empty.")
			.MinimumLength(8).WithMessage("Passwords must be at least 8 characters long.")
			.MaximumLength(24).WithMessage("Passwords length cannot exceed 24 characters.")
			.Matches(@"[A-Z]+").WithMessage("Passwords must contain at least one upper-case letter.")
			.Matches(@"[a-z]+").WithMessage("Passwords must contain at least one lower-case letter.")
			.Matches(@"[0-9]+").WithMessage("Passwords must contain at least one number.")
			.Matches(@"[\!\?\*\.]+").WithMessage("Passwords must contain at least one symbol: (!? *.).");
	}
}