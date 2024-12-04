namespace AppApiService.Extensions;

public static class RateLimitExtensions
{
	const string policy = "PerUserRatelimit";

	public static IServiceCollection AddRateLimiting(this IServiceCollection services)
	{
		return services.AddRateLimiter(options =>
		{
			options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

			_ = options.AddPolicy(policy, context =>
			{
				// We always have a user name
				string username = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

				return RateLimitPartition.GetTokenBucketLimiter(username, _ =>
				{
					return new()
					{
						ReplenishmentPeriod = TimeSpan.FromSeconds(10),
						AutoReplenishment = true,
						TokenLimit = 100,
						TokensPerPeriod = 100,
						QueueLimit = 100,
					};
				});
			});
		});
	}

	public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder)
	{
		return builder.RequireRateLimiting(policy);
	}
}