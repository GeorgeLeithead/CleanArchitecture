namespace Application.Todos.GetById;

sealed class GetTodoByIdQueryHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetTodoByIdQuery, TodoResponse>
{
	public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
	{
		TodoResponse? todo = await context.TodoItems
			.Where(todoItem => todoItem.Id == query.TodoItemId && todoItem.UserId == userContext.UserId)
			.Select(todoItem => new TodoResponse
			{
				Id = todoItem.Id,
				UserId = todoItem.UserId,
				Description = todoItem.Description,
				DueDate = todoItem.DueDate == null ? null : ((DateTimeOffset)todoItem.DueDate).LocalDateTime,
				Labels = todoItem.Labels.AsReadOnly(),
				IsCompleted = todoItem.IsCompleted,
				CreatedAt = ((DateTimeOffset)todoItem.CreatedAt).LocalDateTime,
				CompletedAt = todoItem.CompletedAt == null ? null : ((DateTimeOffset)todoItem.CompletedAt).LocalDateTime,
			})
			.SingleOrDefaultAsync(cancellationToken);

		return todo is null ? Result.Failure<TodoResponse>(TodoItemErrors.NotFound(query.TodoItemId)) : (Result<TodoResponse>)todo;
	}
}