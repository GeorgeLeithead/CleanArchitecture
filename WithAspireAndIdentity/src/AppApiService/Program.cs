using System.Text.Json.Serialization;
using AppApiService;
using AppApiService.Extensions;
using Application;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using ServiceDefaults;

ApiVersion[] supportedApiVersions =
[
	new(1.0)
];

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddSqlServerDbContext<ApplicationDbContext>("sqldb");

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services
	.AddApplication()
	.AddPresentation()
	.AddInfrastructure();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

IApiVersioningBuilder withApiVersioning = builder.Services.AddApiVersioning(opt =>
{
	opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));
	opt.DefaultApiVersion = supportedApiVersions[0];
	opt.AssumeDefaultVersionWhenUnspecified = true;
	opt.ReportApiVersions = true;
}).AddApiExplorer(opt =>
{
	opt.GroupNameFormat = "'v'V";
	opt.SubstituteApiVersionInUrl = true;
});
builder.AddDefaultOpenApi(withApiVersioning);

// Configure options for reading and writing JSON
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.SerializerOptions.PropertyNamingPolicy = null;
}
);

//// Add services to the container.
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();

// Configure rate limiting
builder.Services.AddRateLimiting();

builder.Services.AddHttpLogging(o =>
{
	if (builder.Environment.IsDevelopment())
	{
		o.CombineLogs = true;
		o.LoggingFields = HttpLoggingFields.ResponseBody | HttpLoggingFields.ResponseHeaders;
	}
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseHttpLogging();
app.UseRateLimiter();

// Declare the API version set
ApiVersionSet apiVersionSet = app.NewApiVersionSet()
	.HasApiVersion(supportedApiVersions[0])
	.ReportApiVersions()
	.Build();

// Register the routing group
RouteGroupBuilder versionedGroup = app
	.MapGroup("v{version:apiVersion}")
	.WithApiVersionSet(apiVersionSet);

app.UseHttpsRedirection();

// Map the endpoints
app.MapDefaultEndpoints();
app.MapEndpoints(versionedGroup);
app.MapGroup("/account").MapIdentityApi<AppUser>();
app.UseDefaultOpenApi();

app.UseRequestContextLogging();

app.UseAuthentication();
//app.UseAuthorization();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace AppApiService
{
	sealed partial class Program;
}