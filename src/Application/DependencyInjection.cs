namespace Application;

/// <summary>Application dependency injection.</summary>
public static class ApplicationDependencyInjection
{
	/// <summary>Application.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>An <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		_ = services.AddMediatR(config =>
		{
			_ = config.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
			_ = config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
		});

		_ = services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly, includeInternalTypes: true);

		return services;
	}
}