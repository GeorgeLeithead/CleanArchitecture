// Ignore Spelling: api, Authorisation, Serilog
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ApiVersion[] supportedApiVersions =
[
	new(1.0)
];

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
	.AddApplication()
	.AddPresentation()
	.AddInfrastructure(builder.Configuration);
builder.AddBasicServiceDefaults();

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

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

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
////app.UseCors();

// Map the endpoints
app.MapDefaultEndpoints();
app.MapEndpoints(versionedGroup);
app.UseDefaultOpenApi();

if (app.Environment.IsDevelopment())
{
	app.ApplyMigrations();
}

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

// REMARK: If you want to use Controllers, you'll need this.
////app.MapControllers();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace Web.Api
{
	/// <summary>Program.</summary>
	sealed partial class Program;
}