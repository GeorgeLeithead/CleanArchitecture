using MigrationService;
using Application;
using Application.Abstractions.Authentication;
using Infrastructure.Authentication;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.AddSqlServerDbContext<ApplicationDbContext>("sqldb");

builder.AddServiceDefaults();
builder.Services
	.AddApplication();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry()
	.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

IHost host = builder.Build();
await host.RunAsync();
