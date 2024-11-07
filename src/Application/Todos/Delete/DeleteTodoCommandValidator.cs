namespace Application.Todos.Delete;

sealed class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
	public DeleteTodoCommandValidator() => RuleFor(c => c.TodoItemId).NotEmpty().WithMessage("Item identifier is required.");
}