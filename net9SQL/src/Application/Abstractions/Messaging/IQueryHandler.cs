namespace Application.Abstractions.Messaging;

/// <summary>Query handler interface.</summary>
/// <typeparam name="TQuery">Type query.</typeparam>
/// <typeparam name="TResponse">Type response.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>;