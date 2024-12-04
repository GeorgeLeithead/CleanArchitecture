namespace AppApiService.Middleware;

sealed class RequestContextLoggingMiddleware(RequestDelegate next)
{
	const string correlationIdHeaderName = "Correlation-Id";

	public Task Invoke(HttpContext context)
	{
		using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
		{
			return next.Invoke(context);
		}
	}

	static string GetCorrelationId(HttpContext context)
	{
		_ = context.Request.Headers.TryGetValue(correlationIdHeaderName, out StringValues correlationId);
		return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
	}
}