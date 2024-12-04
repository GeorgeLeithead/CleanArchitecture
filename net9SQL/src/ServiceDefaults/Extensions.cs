// Ignore Spelling: Polly
namespace ServiceDefaults;

#pragma warning disable CA1724 // The type name Extensions conflicts in whole or in part with the namespace name 'Microsoft.AspNetCore.Builder.Extensions'
public static partial class Extensions
#pragma warning restore CA1724 // The type name Extensions conflicts in whole or in part with the namespace name 'Microsoft.AspNetCore.Builder.Extensions'
{
	/// <summary>Adds the default services.</summary>
	/// <param name="builder">Host application builder.</param>
	/// <returns>Enhanced host application builder.</returns>
	public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		_ = builder.AddBasicServiceDefaults();
		_ = builder.Services.AddServiceDiscovery();

		_ = builder.Services.ConfigureHttpClientDefaults(http =>
		{
			// Turn on resilience by default
			_ = http.AddStandardResilienceHandler();

			// Turn on service discovery by default
			_ = http.AddServiceDiscovery();
		});

		// Comment out the following to remove the restriction for the allowed schemes for service discovery.
		_ = builder.Services.Configure<ServiceDiscoveryOptions>(options => options.AllowedSchemes = ["https"]);

		return builder;
	}

	/// <summary>Configure open telemetry.</summary>
	/// <param name="builder">Host application builder.</param>
	/// <returns>Enhanced host application builder.</returns>
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
				if (builder.Environment.IsDevelopment())
				{
					// We want to view all traces in development
					_ = tracing.SetSampler(new AlwaysOnSampler());
				}
				_ = tracing.AddSource(builder.Environment.ApplicationName)
					.AddAspNetCoreInstrumentation()
					// Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
					//.AddGrpcClientInstrumentation()
					.AddHttpClientInstrumentation();
			});

		_ = builder.AddOpenTelemetryExporters();

		return builder;
	}

	/// <summary>Adds the services except for making outgoing HTTP calls.</summary>
	/// <remarks>This allows for things like Polly to be trimmed out of the app if it isn't used.</remarks>
	public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
	{
		// Default health checks assume the event bus and self health checks
		_ = builder.AddDefaultHealthChecks();
		_ = builder.ConfigureOpenTelemetry();

		return builder;
	}

	static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
	{
		bool useExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
		if (useExporter)
		{
			// TODO: Check if I need this:
			////_ = builder.Services.AddOpenTelemetry().UseOtlpExporter();
			_ = builder.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
			_ = builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
			_ = builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
		}

		//// Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
		////if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
		////{
		////    builder.Services.AddOpenTelemetry()
		////       .UseAzureMonitor();
		////}

		return builder;
	}

	/// <summary>Add default health checks.</summary>
	/// <param name="builder">Host application builder.</param>
	/// <returns>Enhanced builder.</returns>
	public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
	{
		_ = builder.Services.AddHealthChecks()
			// Add a default liveness check to ensure app is responsive
			.AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

		return builder;
	}

	/// <summary>Map default end points.</summary>
	/// <param name="app">Web application.</param>
	/// <returns>Enhanced web application.</returns>
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