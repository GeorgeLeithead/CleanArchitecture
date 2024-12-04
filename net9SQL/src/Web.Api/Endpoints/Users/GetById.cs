namespace Web.Api.Endpoints.Users;

sealed class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapGet("users/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
			{
				GetUserByIdQuery query = new(userId);

				Result<UserResponse> result = await sender.Send(query, cancellationToken);

				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.HasPermission(Permissions.UsersAccess)
		.WithTags(Tags.Users)
		.MapToApiVersion(new ApiVersion(1, 0));
}