// Ignore Spelling: v'major,

namespace ServiceDefaults;
using Microsoft.AspNetCore.Routing;
using Scalar.AspNetCore;

/// <summary>Service extensions.</summary>
public static partial class Extensions
{
	/// <summary>Use Open API - Default</summary>
	/// <param name="app">Web application.</param>
	/// <returns>Extended Web Application.</returns>
	public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
	{
		IConfiguration configuration = app.Configuration;
		IConfigurationSection openApiSection = configuration.GetSection("OpenApi");

		if (!openApiSection.Exists())
		{
			return app;
		}

		_ = app.MapOpenApi();

		if (app.Environment.IsDevelopment())
		{
			_ = app.MapScalarApiReference(options =>
			{
				// Disable default fonts to avoid download unnecessary fonts
				options.DefaultFonts = false;
			});
			_ = app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
		}

		return app;
	}

	/// <summary>Add Open API defaults.</summary>
	/// <param name="builder">Host application builder.</param>
	/// <param name="apiVersioning">API versioning builder.</param>
	/// <returns>Enhanced builder.</returns>
	public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder, IApiVersioningBuilder? apiVersioning = default)
	{
		IConfigurationSection openApi = builder.Configuration.GetSection("OpenApi");
		IConfigurationSection identitySection = builder.Configuration.GetSection("Identity");

		Dictionary<string, string?> scopes = identitySection.Exists() ? identitySection.GetRequiredSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value) : [];
		if (!openApi.Exists())
		{
			return builder;
		}

		if (apiVersioning is not null)
		{
			// the default format will just be ApiVersion.ToString(); for example, 1.0.
			// this will format the version as "'v'major[.minor][-status]"
			_ = apiVersioning.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");
			string[] versions = ["v1", "v2"];
			foreach (string description in versions)
			{
				_ = builder.Services.AddOpenApi(description, options =>
				{
					_ = options.ApplyApiVersionInfo(
						openApi.GetRequiredValue("Document:Title"),
						openApi.GetRequiredValue("Document:Description"),
						openApi.GetRequiredValue("Document:ContactName"),
						new Uri(openApi.GetRequiredValue("Document:ContactUrl")),
						openApi.GetRequiredValue("Document:LicenseType"),
						new Uri(openApi.GetRequiredValue("Document:LicenseUrl")),
						new Uri(openApi.GetRequiredValue("Document:TermsOfService"))
						);
					_ = options.ApplyAuthorizationChecks([.. scopes.Keys]);
					_ = options.ApplySecuritySchemeDefinitions();
					_ = options.ApplyOperationDeprecatedStatus();
					// Clear out the default servers so we can fallback to
					// whatever ports have been allocated for the service by Aspire
					_ = options.AddDocumentTransformer((document, _, _) =>
					{
						document.Servers = [];
						return Task.CompletedTask;
					});
				});
			}
		}

		return builder;
	}
}
