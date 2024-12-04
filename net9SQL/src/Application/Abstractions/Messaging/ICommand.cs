namespace Application.Abstractions.Messaging;

#pragma warning disable CA1040 // Avoid empty interfaces
/// <summary>Command interface.</summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>Command interface.</summary>
/// <typeparam name="TResponse">Response type.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

/// <summary>Base command interface.</summary>
public interface IBaseCommand;
#pragma warning restore CA1040 // Avoid empty interfaces