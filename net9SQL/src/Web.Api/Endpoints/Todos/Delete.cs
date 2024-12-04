// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class Delete : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapDelete("todos/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
			{
				DeleteTodoCommand command = new(id);
				Result result = await sender.Send(command, cancellationToken);
				return result.Match(Results.NoContent, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(new ApiVersion(1, 0));
}