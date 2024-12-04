// Ignore Spelling: unhandled

namespace Web.Api.Infrastructure;

sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		using IDisposable? scope = logger.ProcessingTodoScope(DateTime.UtcNow);
		logger.UnhandledException(exception);
		ProblemDetails problemDetails = new()
		{
			Instance = httpContext.Request.Path,
			Status = StatusCodes.Status500InternalServerError,
			Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
			Title = "Server failure"
		};

		httpContext.Response.StatusCode = problemDetails.Status.Value;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true;
	}
}