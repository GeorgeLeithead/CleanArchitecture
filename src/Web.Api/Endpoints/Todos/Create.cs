// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class Create : IEndpoint
{
	sealed record Request(Guid UserId, string Description, Priority Priority, List<string> Labels, DateTime? DueDate);

	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPost("todos", async (Request request, ISender sender, CancellationToken cancellationToken) =>
			{
				CreateTodoCommand command = new(request.UserId, request.Description, request.Priority, request.Labels.AsReadOnly(), request.DueDate);
				Result<Guid> result = await sender.Send(command, cancellationToken);
				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(new ApiVersion(1, 0));
}