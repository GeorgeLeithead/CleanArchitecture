namespace ServiceDefaults;

/// <summary>HTTP client extensions.</summary>
public static class HttpClientExtensions
{
	/// <summary>Add authentication token.</summary>
	/// <param name="builder">HTTP Client builder.</param>
	/// <returns>Enhanced builder</returns>
	public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
	{
		_ = builder.Services.AddHttpContextAccessor();
		builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();
		_ = builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

		return builder;
	}

	sealed class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
	{
		readonly IHttpContextAccessor httpContextAccessor;

#pragma warning disable S1144 // Unused private types or members should be removed
		public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor) => this.httpContextAccessor = httpContextAccessor;

		public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, HttpMessageHandler innerHandler) : base(innerHandler) => this.httpContextAccessor = httpContextAccessor;
#pragma warning restore S1144 // Unused private types or members should be removed

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (httpContextAccessor.HttpContext is HttpContext context)
			{
				string? accessToken = await context.GetTokenAsync("access_token");
				if (accessToken is not null)
				{
					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				}
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}