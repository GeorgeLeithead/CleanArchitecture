namespace Web.Api.Endpoints.Users;

sealed class Register : IEndpoint
{
	sealed record Request(string Email, string FirstName, string LastName, string Password);

	public void MapEndpoint(IEndpointRouteBuilder app) => _ = app
		.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
			{
				RegisterUserCommand command = new(
					request.Email,
					request.FirstName,
					request.LastName,
					request.Password);

				Result<Guid> result = await sender.Send(command, cancellationToken);

				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Users)
		.MapToApiVersion(new ApiVersion(1, 0));
}