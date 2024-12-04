namespace Infrastructure.Authorization;

/// <summary>Has permission attribute.</summary>
/// <param name="permission">permission</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
	/// <summary>Permission</summary>
	public string Permission { get; } = string.Empty;
}