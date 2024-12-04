namespace Application.Todos.Complete;

sealed class CompleteTodoCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider, IUserContext userContext) : ICommandHandler<CompleteTodoCommand>
{
	public async Task<Result> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
	{
		TodoItem? todoItem = await context.TodoItems.SingleOrDefaultAsync(t => t.Id == command.TodoItemId && t.UserId == userContext.UserId, cancellationToken);
		if (todoItem is null)
		{
			return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
		}

		if (todoItem.IsCompleted)
		{
			return Result.Failure(TodoItemErrors.AlreadyCompleted(command.TodoItemId));
		}

		todoItem.IsCompleted = true;
		todoItem.CompletedAt = dateTimeProvider.UtcNow;

		todoItem.AddDomainEvent(new TodoItemCompletedDomainEvent(todoItem.Id));

		_ = await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}