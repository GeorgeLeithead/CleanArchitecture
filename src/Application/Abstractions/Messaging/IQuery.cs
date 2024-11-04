namespace Application.Abstractions.Messaging;

/// <summary>Query interface.</summary>
/// <typeparam name="TResponse">Type response.</typeparam>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
#pragma warning restore CA1040 // Avoid empty interfaces