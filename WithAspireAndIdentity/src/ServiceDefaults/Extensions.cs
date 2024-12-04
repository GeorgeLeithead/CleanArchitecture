#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.Hosting;

#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.</summary>
/// <remarks>This project should be referenced by each service project in your solution. To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults.</remarks>
public static partial class Extensions
{
	public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		_ = builder.ConfigureOpenTelemetry();

		_ = builder.AddDefaultHealthChecks();

		_ = builder.Services.AddServiceDiscovery();

		_ = builder.Services.ConfigureHttpClientDefaults(http =>
		{
			// Turn on resilience by default
			_ = http.AddStandardResilienceHandler();

			// Turn on service discovery by default
			_ = http.AddServiceDiscovery();
		});

		// Uncomment the following to restrict the allowed schemes for service discovery.
		_ = builder.Services.Configure<ServiceDiscoveryOptions>(options => options.AllowedSchemes = ["https"]);

		return builder;
	}

	public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		_ = builder.Logging.AddOpenTelemetry(logging =>
		{
			logging.IncludeFormattedMessage = true;
			logging.IncludeScopes = true;
		});

		_ = builder.Services.AddOpenTelemetry()
			.WithMetrics(metrics =>
			{
				_ = metrics.AddAspNetCoreInstrumentation()
					.AddHttpClientInstrumentation()
					.AddRuntimeInstrumentation();
			})
			.WithTracing(tracing =>
			{
				_ = tracing.AddSource(builder.Environment.ApplicationName)
					.AddAspNetCoreInstrumentation()
					//// Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
					////.AddGrpcClientInstrumentation()
					.AddHttpClientInstrumentation();
			});

		_ = builder.AddOpenTelemetryExporters();

		return builder;
	}

	static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		bool useOTLPExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

		if (useOTLPExporter)
		{
			_ = builder.Services.AddOpenTelemetry().UseOtlpExporter();
		}

		//// Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
		////if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
		////{
		////    builder.Services.AddOpenTelemetry()
		////       .UseAzureMonitor();
		////}

		return builder;
	}

	public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		_ = builder.Services.AddHealthChecks()
			// Add a default liveness check to ensure app is responsive
			.AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

		return builder;
	}

	public static WebApplication MapDefaultEndpoints(this WebApplication app)
	{
		// Adding health checks endpoints to applications in non-development environments has security implications.
		// See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
		if (app.Environment.IsDevelopment())
		{
			// All health checks must pass for app to be considered ready to accept traffic after starting
			_ = app.MapHealthChecks("/health");

			// Only health checks tagged with the "live" tag must pass for app to be considered alive
			_ = app.MapHealthChecks("/alive", new HealthCheckOptions
			{
				Predicate = r => r.Tags.Contains("live")
			});
		}

		return app;
	}
}
