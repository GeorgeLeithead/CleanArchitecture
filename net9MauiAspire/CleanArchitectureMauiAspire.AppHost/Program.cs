var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.CleanArchitectureMauiAspire_ApiService>("apiservice");

// MAUI projects are registered differently than other project types, with AddMobileProject. Aspire settings
// that are normally set as environment variables at launch time are in the case of MAUI instead generated
// in the specified MAUI app project directory, in the AspireAppSettings.g.cs file.
builder.AddMobileProject("mauiclient", "../CleanArchitectureMauiAspire")
    .WithReference(apiService);

builder.Build().Run();
