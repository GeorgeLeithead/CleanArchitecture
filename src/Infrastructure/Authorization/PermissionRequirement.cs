namespace Infrastructure.Authorization;

sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
	public string Permission { get; } = permission;
}