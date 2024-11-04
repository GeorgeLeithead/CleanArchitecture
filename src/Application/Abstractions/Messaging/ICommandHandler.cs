namespace Application.Abstractions.Messaging;

/// <summary>Command handler interface.</summary>
/// <typeparam name="TCommand">Type command.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand;

/// <summary>Command handler interface.</summary>
/// <typeparam name="TCommand">Type command.</typeparam>
/// <typeparam name="TResponse">Type response.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>;