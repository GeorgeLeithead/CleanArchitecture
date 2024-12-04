namespace Application.Todos.GetById;

sealed class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
{
	public GetTodoByIdQueryValidator() => RuleFor(c => c.TodoItemId).NotEmpty().WithMessage("Item identifier is required.");
}