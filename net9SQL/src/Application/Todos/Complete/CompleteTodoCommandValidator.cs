namespace Application.Todos.Complete;

sealed class CompleteTodoCommandValidator : AbstractValidator<CompleteTodoCommand>
{
	public CompleteTodoCommandValidator() => RuleFor(c => c.TodoItemId).NotEmpty().WithMessage("Item identifier is required.");
}