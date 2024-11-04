// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class Complete : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPut("todos/{id:guid}/complete", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
			{
				CompleteTodoCommand command = new(id);
				Result result = await sender.Send(command, cancellationToken);
				return result.Match(Results.NoContent, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(1);
}
