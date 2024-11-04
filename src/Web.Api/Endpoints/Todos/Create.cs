// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

sealed class Create : IEndpoint
{
	sealed record Request(Guid UserId, string Description, DateTime? DueDate, List<string> Labels, int Priority);

	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPost("todos", async (Request request, ISender sender, CancellationToken cancellationToken) =>
			{
				CreateTodoCommand command = new()
				{
					UserId = request.UserId,
					Description = request.Description,
					DueDate = request.DueDate,
					Labels = request.Labels,
					Priority = (Priority)request.Priority
				};

				Result<Guid> result = await sender.Send(command, cancellationToken);

				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Todos)
		.RequireAuthorization()
		.MapToApiVersion(1);
}
