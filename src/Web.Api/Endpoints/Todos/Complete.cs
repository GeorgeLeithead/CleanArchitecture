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
		.WithName("CompleteTodoById")
		.WithTags(Tags.Todos)
		.WithOpenApi(generatedOperation =>
			{
				generatedOperation.Summary = "Complete a To do by Id";
				OpenApiParameter? parameter = generatedOperation.Parameters[0];
				parameter.Description = "Complete a to do item for the specified identifier,";
				parameter.Required = true;
				return generatedOperation;
			})
		.RequireAuthorization()
		.MapToApiVersion(1);
}
