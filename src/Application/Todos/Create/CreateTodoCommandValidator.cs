namespace Application.Todos.Create;

/// <summary>Create a to do command validator</summary>
public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
	/// <summary>Initializes a new instance of the <see cref="CreateTodoCommandValidator" /> class.</summary>
	public CreateTodoCommandValidator()
	{
		_ = RuleFor(c => c.UserId).NotEmpty();
		_ = RuleFor(c => c.Priority).IsInEnum();
		_ = RuleFor(c => c.Description).NotEmpty().MaximumLength(255);
		_ = RuleFor(c => c.DueDate).GreaterThanOrEqualTo(DateTime.Today).When(x => x.DueDate.HasValue);
	}
}
