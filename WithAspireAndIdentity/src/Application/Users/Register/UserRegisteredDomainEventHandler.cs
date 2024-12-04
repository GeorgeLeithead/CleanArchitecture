namespace Application.Users.Register;

sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
	public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
	{
		 //TODO: Send email from here...
		return Task.CompletedTask;
	}
}