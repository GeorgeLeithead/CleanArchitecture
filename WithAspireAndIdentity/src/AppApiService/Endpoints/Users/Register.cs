namespace AppApiService.Endpoints.Users;

sealed class Register : IEndpoint
{
	sealed record Request(string Email, string Password, string ConfirmPassword);

	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
		{
			RegisterUserCommand command = new(
				request.Email,
				request.Password,
				request.ConfirmPassword);

			Result<Guid> result = await sender.Send(command, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Users)
		.MapToApiVersion(new ApiVersion(1, 0));
}