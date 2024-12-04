namespace Infrastructure.Authorization;

public static class AuthorizationHandlerExtensions
{
	public static AuthorizationBuilder AddCurrentUserHandler(this AuthorizationBuilder builder)
	{
		_ = builder.Services.AddScoped<IAuthorizationHandler, CheckCurrentUserAuthHandler>();
		return builder;
	}

	/// <summary>Adds the current user requirement that will activate our authorization handler.</summary>
	/// <param name="builder">Authorization policy builder.</param>
	/// <returns>Enhanced builder.</returns>
	public static AuthorizationPolicyBuilder RequireCurrentUser(this AuthorizationPolicyBuilder builder)
	{
		return builder.RequireAuthenticatedUser()
					  .AddRequirements(new CheckCurrentUserRequirement());
	}

	class CheckCurrentUserRequirement : IAuthorizationRequirement;

	/// <summary>This authorization handler verifies that the user exists even if there's a valid token.</summary>
	/// <param name="currentUser">Current user.</param>
	class CheckCurrentUserAuthHandler(CurrentUser currentUser) : AuthorizationHandler<CheckCurrentUserRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckCurrentUserRequirement requirement)
		{
			// TODO: Check user if the user is locked out as well
			if (currentUser.User is not null)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}