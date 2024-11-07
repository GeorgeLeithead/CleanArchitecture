// Ignore Spelling: todos

namespace Web.Api.Endpoints.Todos;

using Microsoft.AspNetCore.Builder;

sealed class Get : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapGet("todos", async (Guid userId, ISender sender, CancellationToken cancellationToken, int page = 1, int pageSize = 10) =>
		{
			GetTodosQuery command = new(userId, page, pageSize);
			Result<List<Application.Todos.GetAll.TodoResponse>> result = await sender.Send(command, cancellationToken);
			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Todos)
		.WithOpenApi(generatedOperation =>
		{
			generatedOperation.Summary = "Get paged to do's for a user.";
			OpenApiParameter? parameterUserId = generatedOperation.Parameters[0];
			parameterUserId.Description = "User unique identifier";
			parameterUserId.Required = true;
			OpenApiParameter? parameterPage = generatedOperation.Parameters[1];
			parameterPage.Description = "Page number, must be greater than 1.";
			parameterPage.Required = false;
			OpenApiParameter? parameterPageSize = generatedOperation.Parameters[2];
			parameterPageSize.Description = "Page size, must be greater than 1. Default is 10.";
			parameterPageSize.Required = false;
			return generatedOperation;
		})
		.RequireAuthorization()
		.MapToApiVersion(new ApiVersion(1, 0));
}