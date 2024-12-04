// Ignore Spelling: oauth,

namespace ServiceDefaults;

static partial class OpenApiOptionsExtensions
{
	public static OpenApiOptions ApplyApiVersionInfo(this OpenApiOptions options, string title, string description, string contactName, Uri contactUrl, string licenseType, Uri licenseUrl, Uri termsOfServiceUrl)
	{
		_ = options.AddDocumentTransformer((document, context, _) =>
		{
			IApiVersionDescriptionProvider? versionedDescriptionProvider = context.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
			ApiVersionDescription? apiDescription = versionedDescriptionProvider?.ApiVersionDescriptions
				.SingleOrDefault(description => description.GroupName == context.DocumentName);
			if (apiDescription is null)
			{
				return Task.CompletedTask;
			}

			document.Info.Version = apiDescription.ApiVersion.ToString();
			document.Info.Title = title;
			document.Info.Description = BuildDescription(apiDescription, description);
			document.Info.Contact = new OpenApiContact { Name = contactName, Url = contactUrl };
			document.Info.License = new OpenApiLicense { Name = licenseType, Url = licenseUrl };
			document.Info.TermsOfService = termsOfServiceUrl;
			return Task.CompletedTask;
		});
		return options;
	}

	static string BuildDescription(ApiVersionDescription api, string description)
	{
		StringBuilder text = new(description);

		if (api.IsDeprecated)
		{
			if (text.Length > 0)
			{
				if (text[^1] != '.')
				{
					_ = text.Append('.');
				}

				_ = text.Append(' ');
			}

			_ = text.Append("This API version has been deprecated.");
		}

		if (api.SunsetPolicy is { } policy)
		{
			if (policy.Date is { } when)
			{
				if (text.Length > 0)
				{
					_ = text.Append(' ');
				}

				_ = text.Append("The API will be sunset on ")
					.Append(when.Date.ToShortDateString())
					.Append('.');
			}

			if (policy.HasLinks)
			{
				_ = text.AppendLine();

				bool rendered = false;

				foreach (LinkHeaderValue? link in policy.Links.Where(l => l.Type == "text/html"))
				{
					if (!rendered)
					{
						_ = text.Append("<h4>Links</h4><ul>");
						rendered = true;
					}

					_ = text.Append("<li><a href=\"");
					_ = text.Append(link.LinkTarget.OriginalString);
					_ = text.Append("\">");
					_ = text.Append(
						StringSegment.IsNullOrEmpty(link.Title)
						? link.LinkTarget.OriginalString
						: link.Title.ToString());
					_ = text.Append("</a></li>");
				}

				if (rendered)
				{
					_ = text.Append("</ul>");
				}
			}
		}

		return text.ToString();
	}

	public static OpenApiOptions ApplySecuritySchemeDefinitions(this OpenApiOptions options) => options.AddDocumentTransformer<SecuritySchemeDefinitionsTransformer>();

	public static OpenApiOptions ApplyAuthorizationChecks(this OpenApiOptions options, string[] scopes)
	{
		_ = options.AddOperationTransformer((operation, context, cancellationToken) =>
		{
			IList<object> metadata = context.Description.ActionDescriptor.EndpointMetadata;

			if (!metadata.OfType<IAuthorizeData>().Any())
			{
				return Task.CompletedTask;
			}

			_ = operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
			_ = operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
			OpenApiSecurityScheme oAuthScheme = new()
			{
				Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
			};

			operation.Security =
			[
				new()
				{
					[oAuthScheme] = scopes
				}
			];

			return Task.CompletedTask;
		});
		return options;
	}

	public static OpenApiOptions ApplyOperationDeprecatedStatus(this OpenApiOptions options) => options.AddOperationTransformer((operation, context, _) =>
																									 {
																										 ApiDescription apiDescription = context.Description;
																										 operation.Deprecated |= apiDescription.IsDeprecated();
																										 return Task.CompletedTask;
																									 });
}