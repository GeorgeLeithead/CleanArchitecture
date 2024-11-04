namespace SharedKernel;

/// <summary>Domain event interface.</summary>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface IDomainEvent : INotification;
#pragma warning restore CA1040 // Avoid empty interfaces