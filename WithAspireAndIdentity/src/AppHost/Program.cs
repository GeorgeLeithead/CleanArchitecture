// Ignore Spelling: appApiService, webfrontend, sql, sqldb, sqlcontainer

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

// DB
IResourceBuilder<SqlServerDatabaseResource> sql = builder.AddSqlServer("sql")
	.WithContainerName("sqlcontainer")
//	.WithLifetime(ContainerLifetime.Persistent)
	.AddDatabase("sqldb");

// Data migrations
IResourceBuilder<ProjectResource> migrations = builder.AddProject<Projects.MigrationService>("migrations")
	.WithReference(sql)
	.WaitFor(sql);

// API service
IResourceBuilder<ProjectResource> appApiService = builder.AddProject<Projects.AppApiService>("appapiservice")
	.WithReference(sql)
	.WaitFor(sql)
	.WaitForCompletion(migrations);

// Web Front-end
builder.AddProject<Projects.Web>("webfrontend")
	.WithExternalHttpEndpoints()
	.WithReference(sql)
	.WithReference(appApiService)
	.WaitFor(appApiService);

await builder.Build().RunAsync();