namespace Application.Users.Register;

sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
	// TODO: Send an email verification link, etc.
	public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
}