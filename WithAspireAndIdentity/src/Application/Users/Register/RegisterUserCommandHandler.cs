namespace Application.Users.Register;

sealed class RegisterUserCommandHandler(IUserStore<AppUser> userStore, IPasswordHasher passwordHasher, UserManager<AppUser> userManager) : ICommandHandler<RegisterUserCommand, Guid>
{
	public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		AppUser user = new()
		{
			Id = Guid.NewGuid().ToString(),
			Email = command.Email,
			PasswordHash = passwordHasher.Hash(command.Password)
		};

		string? alreadyExistsResult = await userStore.GetUserIdAsync(user, cancellationToken);
		if (!string.IsNullOrEmpty(alreadyExistsResult))
		{
			return Result.Failure<Guid>(UserErrors.EmailNotUnique);
		}

		await userStore.SetUserNameAsync(user, command.Email, CancellationToken.None);
		IUserEmailStore<AppUser> emailStore = (IUserEmailStore<AppUser>)userStore;
		await emailStore.SetEmailAsync(user, command.Email, CancellationToken.None);
		IdentityResult result = await userManager.CreateAsync(user, command.Password);
		if (!result.Succeeded)
		{
			IdentityError firstError = result.Errors.First();
			return Result.Failure<Guid>(new Error(firstError.Code, firstError.Description, ErrorType.Failure));
		}

		user.AddDomainEvent(new UserRegisteredDomainEvent(Guid.Parse(user.Id)));
		return Guid.Parse(user.Id);
	}
}