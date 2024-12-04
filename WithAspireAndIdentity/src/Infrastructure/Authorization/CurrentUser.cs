namespace Infrastructure.Authorization;

using Domain.Users;

/// <summary>A scoped service that exposes the current user information.</summary>
public class CurrentUser
{
	public AppUser? User { get; set; }

	public ClaimsPrincipal Principal { get; set; } = default!;

	public string Id => Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
}