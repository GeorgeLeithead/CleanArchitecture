namespace Application.Todos.Create;

/// <summary>Create a to do command validation rules.</summary>
public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
	/// <summary>Initializes a new instance of the <see cref="CreateTodoCommandValidator" /> class.</summary>
	public CreateTodoCommandValidator()
	{
		_ = RuleFor(c => c.UserId).NotEmpty().WithMessage("User Identifier is required.");
		_ = RuleFor(c => c.Priority).IsInEnum().WithMessage("Priority must be of the correct type.");
		_ = RuleFor(c => c.Description)
			.NotEmpty().WithMessage("Description must not be empty")
			.MinimumLength(3).WithMessage("Description must be at least 3 characters.")
			.MaximumLength(255).WithMessage("Description cannot exceed 255 characters.");
		_ = RuleForEach(c => c.Labels).NotEmpty().WithMessage("One or more labels is required.");
	}
}