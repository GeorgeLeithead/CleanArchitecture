namespace Infrastructure.Authorization;
sealed class PermissionProvider
{
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
#pragma warning disable CA1822 // Mark members as static
	public Task<HashSet<string>> GetForUserIdAsync(Guid userId)
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
	{
		// TODO: Here you'll implement your logic to fetch permissions.
		HashSet<string> permissionsSet = [];
		_ = permissionsSet.Add("users:access");
		HashSet<string> permissionsSetEmpty = [];

		return userId == Guid.Empty ? Task.FromResult(permissionsSetEmpty) : Task.FromResult(permissionsSet);
	}
}