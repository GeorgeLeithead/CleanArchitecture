namespace Infrastructure.Authentication;

static class ClaimsPrincipalExtensions
{
	public static Guid GetUserId(this ClaimsPrincipal? principal)
	{
		string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
		return Guid.TryParse(userId, out Guid parsedUserId) ? parsedUserId : throw new SecurityTokenException("User id is unavailable");
	}
}