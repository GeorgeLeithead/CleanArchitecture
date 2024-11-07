namespace Web.Api.Endpoints.Users;

sealed class Login : IEndpoint
{
	sealed record Request(string Email, string Password);

	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPost("users/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
			{
				LoginUserCommand command = new(request.Email, request.Password);

				Result<string> result = await sender.Send(command, cancellationToken);

				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Users)
		.MapToApiVersion(new ApiVersion(1, 0));
}