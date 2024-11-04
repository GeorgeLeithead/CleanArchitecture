// Ignore Spelling: Accessor

namespace Infrastructure.Authentication;

sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
	readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

	public Guid UserId => httpContextAccessor
		.HttpContext?
		.User
		.GetUserId() ?? throw new ArgumentNullException(nameof(httpContextAccessor), "User context is unavailable");
}