namespace Infrastructure;

/// <summary>Application Dependency Injection</summary>
public static class ApplicationDependencyInjection
{
	/// <summary>Add infrastructure.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>An <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		return services
			.AddServices()
			//.AddDatabase()
			.AddAuthenticationInternal()
			.AddAuthorizationInternal();
	}

	static IServiceCollection AddServices(this IServiceCollection services)
	{
		_ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		_ = services.AddTransient<IEmailSender, EmailSender>();
		_ = services.Configure<EmailSenderOptions>(options =>
		{
			options.HostAddress = "smtp.live.com";
			options.HostPort = 587;
			options.HostUsername = "my-smtp-username";
			options.HostPassword = "my-smtp-password";
			options.SenderEMail = "noreply@mydomain.com";
			options.SenderName = "My Sender Name";
		});
		_ = services.AddSingleton(new SmtpClient
		{
			Host = "smtp.live.com",
			Port = 587,
			Credentials = new NetworkCredential("george@internetwideworld.com", "your-email-password"),
			EnableSsl = true
		});
		return services;
	}

	static IServiceCollection AddDatabase(this IServiceCollection services)
	{
		_ = services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
		return services;
	}

	static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
	{
		// Configure auth
		_ = services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
		// Configure identity
		_ = services.AddAuthorizationBuilder().AddCurrentUserHandler();
		_ = services.AddSingleton<IPasswordHasher, PasswordHasher>();

		return services;
	}

	static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
	{
		// Configure identity
		_ = services.AddIdentityCore<AppUser>()
						.AddEntityFrameworkStores<ApplicationDbContext>()
						.AddApiEndpoints();
		// State that represents the current user from the database *and* the request
		_ = services.AddCurrentUser();
		return services;
	}
}