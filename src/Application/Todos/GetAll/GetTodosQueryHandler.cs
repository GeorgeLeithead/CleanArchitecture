namespace Application.Todos.GetAll;

using Application.Extensions;

sealed class GetTodosQueryHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetTodosQuery, List<TodoResponse>>
{
	public async Task<Result<List<TodoResponse>>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
	{
		if (query.UserId != userContext.UserId)
		{
			return Result.Failure<List<TodoResponse>>(UserErrors.Unauthorized());
		}

		List<TodoResponse> todos = await context.TodoItems
			.Where(todoItem => todoItem.UserId == query.UserId)
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
			.Page(query.page, query.pageSize)
			.ToListAsync(cancellationToken);

		return todos;
	}
}