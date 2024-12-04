namespace Infrastructure.Authorization;

sealed class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
	readonly AuthorizationOptions authorizationOptions = options.Value;

	public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
	{
		AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

		if (policy is not null)
		{
			return policy;
		}

		AuthorizationPolicy permissionPolicy = new AuthorizationPolicyBuilder()
			.AddRequirements(new PermissionRequirement(policyName))
			.Build();

		authorizationOptions.AddPolicy(policyName, permissionPolicy);
		return permissionPolicy;
	}
}