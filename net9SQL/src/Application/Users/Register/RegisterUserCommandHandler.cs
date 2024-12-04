namespace Application.Users.Register;

sealed class RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, Guid>
{
	public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
		{
			return Result.Failure<Guid>(UserErrors.EmailNotUnique);
		}

		User user = new()
		{
			Id = Guid.NewGuid(),
			Email = command.Email,
			FirstName = command.FirstName,
			LastName = command.LastName,
			PasswordHash = passwordHasher.Hash(command.Password)
		};

		user.AddDomainEvent(new UserRegisteredDomainEvent(user.Id));
		_ = context.Users.Add(user);
		_ = await context.SaveChangesAsync(cancellationToken);
		return user.Id;
	}
}