// Ignore Spelling: json
namespace AppApiService.Extensions;

sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
	public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
	{
		IEnumerable<AuthenticationScheme> authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
		if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
		{
			Dictionary<string, OpenApiSecurityScheme> requirements = new()
			{
				["Bearer"] = new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "bearer", // "bearer" refers to the header name here
					In = ParameterLocation.Header,
					BearerFormat = "Json Web Token"
				}
			};
			document.Components ??= new OpenApiComponents();
			document.Components.SecuritySchemes = requirements;
		}
	}
}