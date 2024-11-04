namespace Web.Api.Extensions;

static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
	{
		_ = app.UseSwagger();
		_ = app.UseSwaggerUI();

		return app;
	}
}
