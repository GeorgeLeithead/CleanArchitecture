// Ignore Spelling: oauth,

namespace ServiceDefaults;
using Microsoft.AspNetCore.OpenApi;

static partial class OpenApiOptionsExtensions
{
	sealed class SecuritySchemeDefinitionsTransformer(IConfiguration configuration) : IOpenApiDocumentTransformer
	{
		public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
		{
			//var securitySchema =
			//			new OpenApiSecurityScheme
			//			{
			//				Type = SecuritySchemeType.Http,
			//				Scheme = "bearer",
			//				BearerFormat = "JWT",
			//				Description = "JWT Authorization header using the Bearer scheme."
			//			};

			//var securityRequirement =
			//	new OpenApiSecurityRequirement
			//	{
			//	{
			//		new OpenApiSecurityScheme
			//		{
			//			Reference = new OpenApiReference
			//			{
			//				Id = "AccessToken",
			//				Type = ReferenceType.SecurityScheme
			//			}
			//		},
			//		[]
			//	}
			//	};

			//document.SecurityRequirements.Add(securityRequirement);
			//document.Components = new OpenApiComponents()
			//{
			//	SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>()
			//{
			//	{ "bearer", securitySchema }
			//}
			//};
			//return Task.CompletedTask;

			IConfigurationSection identitySection = configuration.GetSection("Identity");
			if (!identitySection.Exists())
			{
				return Task.CompletedTask;
			}

			string identityUrlExternal = identitySection.GetRequiredValue("Url");
			Dictionary<string, string?> scopes = identitySection.GetRequiredSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value);
			OpenApiSecurityScheme securityScheme = new()
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows()
				{
					// TODO: Change this to use Authorization Code flow with PKCE
					Implicit = new OpenApiOAuthFlow()
					{
						AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
						TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
						Scopes = scopes,
					}
				}
			};
			document.Components ??= new();
			document.Components.SecuritySchemes.Add("oauth2", securityScheme);
			return Task.CompletedTask;
		}
	}
}