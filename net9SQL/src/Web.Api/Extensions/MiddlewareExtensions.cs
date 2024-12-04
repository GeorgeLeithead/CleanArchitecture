namespace Web.Api.Extensions;

static class MiddlewareExtensions
{
	public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app) => app.UseMiddleware<RequestContextLoggingMiddleware>();
}