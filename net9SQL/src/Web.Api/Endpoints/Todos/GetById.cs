// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapGet("todos/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
			{
				GetTodoByIdQuery command = new(id);
				Result<Application.Todos.GetById.TodoResponse> result = await sender.Send(command, cancellationToken);
				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(new ApiVersion(1, 0));
}