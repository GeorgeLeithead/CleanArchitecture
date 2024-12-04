// Ignore Spelling: jwt

namespace Infrastructure;

/// <summary>Application Dependency Injection</summary>
public static class ApplicationDependencyInjection
{
	/// <summary>Add infrastructure.</summary>
	/// <param name="services">Service collection.</param>
	/// <param name="configuration">Configuration.</param>
	/// <returns>An <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
		services
			.AddServices()
			.AddDatabase(configuration)
			.AddHealthChecks(configuration)
			.AddAuthenticationInternal(configuration)
			.AddAuthorizationInternal();

	static IServiceCollection AddServices(this IServiceCollection services)
	{
		_ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		return services;
	}

	static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("Database");
		_ = services.AddDbContext<ApplicationDbContext>(
			options => options
			.UseSqlServer(connectionString, connectionOptions =>
				connectionOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

		_ = services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
		return services;
	}

	static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
	{
		_ = services
			.AddHealthChecks()
			.AddSqlServer(configuration.GetConnectionString("Database")!);
		return services;
	}

	static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, IConfiguration configuration)
	{
		string key = Environment.GetEnvironmentVariable("Jwt_Secret") ?? string.Empty; //// ?? throw new ArgumentException("JWT key is not configured.");
		_ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					ClockSkew = TimeSpan.Zero
				};
			});

		_ = services.AddHttpContextAccessor();
		_ = services.AddScoped<IUserContext, UserContext>();
		_ = services.AddSingleton<IPasswordHasher, PasswordHasher>();
		_ = services.AddSingleton<ITokenProvider, TokenProvider>();

		return services;
	}

	static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
	{
		_ = services.AddAuthorization();
		_ = services.AddScoped<PermissionProvider>();
		_ = services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
		_ = services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

		return services;
	}
}