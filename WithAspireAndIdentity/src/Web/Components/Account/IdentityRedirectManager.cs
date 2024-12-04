namespace Web.Components.Account;

sealed class IdentityRedirectManager(NavigationManager navigationManager)
{
	public const string StatusCookieName = "Identity.StatusMessage";

	static readonly CookieBuilder statusCookieBuilder = new()
	{
		SameSite = SameSiteMode.Strict,
		HttpOnly = true,
		IsEssential = true,
		MaxAge = TimeSpan.FromSeconds(5),
	};

	[DoesNotReturn]
	public void RedirectTo(string? uri)
	{
		uri ??= "";

		// Prevent open redirects.
		if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
		{
			uri = navigationManager.ToBaseRelativePath(uri);
		}

		// During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
		// So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
		navigationManager.NavigateTo(uri);
		throw new InvalidOperationException($"{nameof(IdentityRedirectManager)} can only be used during static rendering.");
	}

	[DoesNotReturn]
	public void RedirectTo(string uri, Dictionary<string, object?> queryParameters)
	{
		string uriWithoutQuery = navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
		string newUri = navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
		RedirectTo(newUri);
	}

	[DoesNotReturn]
	public void RedirectToWithStatus(string uri, string message, HttpContext context)
	{
		context.Response.Cookies.Append(StatusCookieName, message, statusCookieBuilder.Build(context));
		RedirectTo(uri);
	}

	string CurrentPath => navigationManager.ToAbsoluteUri(navigationManager.Uri).GetLeftPart(UriPartial.Path);

	[DoesNotReturn]
	public void RedirectToCurrentPage()
	{
		RedirectTo(CurrentPath);
	}

	[DoesNotReturn]
	public void RedirectToCurrentPageWithStatus(string message, HttpContext context)
	{
		RedirectToWithStatus(CurrentPath, message, context);
	}
}
