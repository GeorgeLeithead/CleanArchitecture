// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class Get : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapGet("todos", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
			{
				GetTodosQuery command = new(userId);
				Result<List<Application.Todos.Read.TodoResponse>> result = await sender.Send(command, cancellationToken);
				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(1);
}
