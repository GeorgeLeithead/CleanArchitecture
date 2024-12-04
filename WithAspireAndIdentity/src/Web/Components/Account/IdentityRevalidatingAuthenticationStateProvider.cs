namespace Web.Components.Account;

using Domain.Users;

// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
// every 30 minutes an interactive circuit is connected.
sealed class IdentityRevalidatingAuthenticationStateProvider(
		ILoggerFactory loggerFactory,
		IServiceScopeFactory scopeFactory,
		IOptions<IdentityOptions> options)
	: RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
	protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

	protected override async Task<bool> ValidateAuthenticationStateAsync(
		AuthenticationState authenticationState, CancellationToken cancellationToken)
	{
		// Get the user manager from a new scope to ensure it fetches fresh data
		await using AsyncServiceScope scope = scopeFactory.CreateAsyncScope();
		UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
		return await ValidateSecurityStampAsync(userManager, authenticationState.User);
	}

	async Task<bool> ValidateSecurityStampAsync(UserManager<AppUser> userManager, ClaimsPrincipal principal)
	{
		AppUser? user = await userManager.GetUserAsync(principal);
		if (user is null)
		{
			return false;
		}
		else if (!userManager.SupportsUserSecurityStamp)
		{
			return true;
		}
		else
		{
			string? principalStamp = principal.FindFirstValue(options.Value.ClaimsIdentity.SecurityStampClaimType);
			string userStamp = await userManager.GetSecurityStampAsync(user);
			return principalStamp == userStamp;
		}
	}
}
