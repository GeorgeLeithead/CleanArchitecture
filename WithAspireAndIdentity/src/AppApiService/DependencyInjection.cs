namespace AppApiService;

static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		_ = services.AddEndpointsApiExplorer();
		_ = services.AddOpenApi(options => _ = options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

		// REMARK: If you want to use Controllers, you'll need this.
		////_ = services.AddControllers();

		_ = services.AddExceptionHandler<GlobalExceptionHandler>();
		_ = services.AddProblemDetails();

		return services;
	}
}