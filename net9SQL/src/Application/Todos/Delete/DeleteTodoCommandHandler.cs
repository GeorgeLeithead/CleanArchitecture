namespace Application.Todos.Delete;

sealed class DeleteTodoCommandHandler(IApplicationDbContext context, IUserContext userContext) : ICommandHandler<DeleteTodoCommand>
{
	public async Task<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
	{
		TodoItem? todoItem = await context.TodoItems
			.SingleOrDefaultAsync(t => t.Id == command.TodoItemId && t.UserId == userContext.UserId, cancellationToken);

		if (todoItem is null)
		{
			return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
		}

		_ = context.TodoItems.Remove(todoItem);
		todoItem.AddDomainEvent(new TodoItemDeletedDomainEvent(todoItem.Id));
		_ = await context.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}