namespace Application.Users;

public class IdentityUserService(UserManager<AppUser> userManager)
{
	public async Task<AppUser?> GetUserByEmailAsync(string email)
	{
		return await userManager.FindByEmailAsync(email);
	}
}