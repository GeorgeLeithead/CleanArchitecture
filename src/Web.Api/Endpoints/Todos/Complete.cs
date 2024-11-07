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
		.WithOpenApi(generatedOperation => new(generatedOperation)
		{
			Summary = "Complete a To do by Id",
			Description = "Mark a to do as complete and with an auto assigned Completed date and time.",
			Parameters = [new OpenApiParameter() { Description = "Complete a to do item for the specified identifier,", Required = true }]
		})
		.RequireAuthorization()
		.MapToApiVersion(new ApiVersion(1, 0));
}