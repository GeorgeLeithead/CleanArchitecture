namespace Web.Api.Extensions;

static class ServiceCollectionExtensions
{
	internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
	{
		_ = services.AddSwaggerGen(o =>
		{
			o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));
			OpenApiSecurityScheme securityScheme = new()
			{
				Name = "JWT Authentication",
				Description = "Enter your JWT token in this field",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT"
			};

			o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
			OpenApiSecurityRequirement securityRequirement = new()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = JwtBearerDefaults.AuthenticationScheme
						}
					},
					[]
				}
			};

			o.AddSecurityRequirement(securityRequirement);
		});

		return services;
	}
}
