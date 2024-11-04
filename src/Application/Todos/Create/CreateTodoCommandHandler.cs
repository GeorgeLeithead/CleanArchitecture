namespace Application.Todos.Create;

sealed class CreateTodoCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider, IUserContext userContext) : ICommandHandler<CreateTodoCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
	{
		if (userContext.UserId != command.UserId)
		{
			return Result.Failure<Guid>(UserErrors.Unauthorized());
		}

		User? user = await context.Users
			.AsNoTracking()
			.SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
		}

		TodoItem todoItem = new()
		{
			UserId = user.Id,
			Description = command.Description,
			Priority = command.Priority,
			DueDate = command.DueDate,
			Labels = command.Labels,
			IsCompleted = false,
			CreatedAt = dateTimeProvider.UtcNow
		};

		todoItem.AddDomainEvent(new TodoItemCreatedDomainEvent(todoItem.Id));

		_ = context.TodoItems.Add(todoItem);
		_ = await context.SaveChangesAsync(cancellationToken);
		return todoItem.Id;
	}
}