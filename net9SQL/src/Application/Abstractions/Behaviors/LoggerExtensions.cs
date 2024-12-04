// Ignore Spelling: unhandled, api

namespace Application.Abstractions.Behaviors;

/// <summary>Logger extensions.</summary>
public static class LoggerExtensions
{
	static readonly Action<ILogger, TodoItem, Exception> processingPriorityTodoItem = LoggerMessage.Define<TodoItem>(LogLevel.Information, new EventId(1, nameof(PriorityTodoItemProcessed)), "Processing priority item: {Item}");
	static readonly Action<ILogger, Exception> failedToProcessTodoItem = LoggerMessage.Define(LogLevel.Critical, new EventId(13, nameof(FailedToProcessTodoItem)), "Epic failure processing item!");
	static readonly Func<ILogger, DateTime, IDisposable?> processingTodoScope = LoggerMessage.DefineScope<DateTime>("Processing work, started at: {DateTime}");
	static readonly Func<ILogger, DateTime, IDisposable?> webApiScope = LoggerMessage.DefineScope<DateTime>("WebApi work, started at: {DateTime}");
	static readonly Action<ILogger, Exception> unhandledException = LoggerMessage.Define(LogLevel.Critical, new EventId(13, nameof(UnhandledException)), "Unhandled exception occurred");

	/// <summary>Priority to do processed.</summary>
	/// <param name="logger">Logger</param>
	/// <param name="todoItem">To do item</param>
	public static void PriorityTodoItemProcessed(this ILogger logger, TodoItem todoItem) => processingPriorityTodoItem(logger, todoItem, default!);

	/// <summary>Failed to process to do item.</summary>
	/// <param name="logger">Logger</param>
	/// <param name="ex">Exception.</param>
	public static void FailedToProcessTodoItem(this ILogger logger, Exception ex) => failedToProcessTodoItem(logger, ex);

	/// <summary>Processing work scope</summary>
	/// <param name="logger">Logger.</param>
	/// <param name="time">Date time.</param>
	/// <returns>Disposable scope.</returns>
	public static IDisposable? ProcessingTodoScope(this ILogger logger, DateTime time) => processingTodoScope(logger, time);

	/// <summary>Unhandled exception.</summary>
	/// <param name="logger">Logger</param>
	/// <param name="ex">Exception.</param>
	public static void UnhandledException(this ILogger logger, Exception ex) => unhandledException(logger, ex);

	/// <summary>Web Api scope.</summary>
	/// <param name="logger">Logger.</param>
	/// <param name="time">Date Time.</param>
	/// <returns>Disposable scope.</returns>
	public static IDisposable? WebApiScope(this ILogger logger, DateTime time) => webApiScope(logger, time);
}