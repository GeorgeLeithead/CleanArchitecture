// Ignore Spelling: unhandled, api

namespace Application.Abstractions.Behaviors;

/// <summary>Logger extensions.</summary>
public static class LoggerExtensions
{
	static readonly Func<ILogger, DateTime, IDisposable?> appApiScope = LoggerMessage.DefineScope<DateTime>("App API work, started at: {DateTime}");
	static readonly Action<ILogger, Exception> unhandledException = LoggerMessage.Define(LogLevel.Critical, new EventId(13, nameof(UnhandledException)), "Unhandled exception occurred");

	/// <summary>Unhandled exception.</summary>
	/// <param name="logger">Logger</param>
	/// <param name="ex">Exception.</param>
	public static void UnhandledException(this ILogger logger, Exception ex) => unhandledException(logger, ex);

	/// <summary>Web Api scope.</summary>
	/// <param name="logger">Logger.</param>
	/// <param name="time">Date Time.</param>
	/// <returns>Disposable scope.</returns>
	public static IDisposable? AppApiScope(this ILogger logger, DateTime time) => appApiScope(logger, time);
}