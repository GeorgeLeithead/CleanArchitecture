namespace MigrationService;

using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using Domain.Users;

public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime, IPasswordHasher passwordHasher) : BackgroundService
{
	public const string ActivitySourceName = "Migrations";
	static readonly ActivitySource activitySource = new(ActivitySourceName);

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		using Activity? activity = activitySource.StartActivity("Migrating database", ActivityKind.Client);

		try
		{
			using IServiceScope scope = serviceProvider.CreateScope();
			ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			await EnsureDatabaseAsync(dbContext, stoppingToken);
			await RunMigrationAsync(dbContext, stoppingToken);
			await SeedDataAsync(dbContext, passwordHasher, stoppingToken);
		}
		catch (Exception ex)
		{
			activity?.RecordException(ex);
			throw;
		}

		hostApplicationLifetime.StopApplication();
	}

	static async Task EnsureDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
	{
		IRelationalDatabaseCreator dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

		IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
		await strategy.ExecuteAsync(async () =>
		{
			// Create the database if it does not exist.
			// Do this first so there is then a database to start a transaction against.
			if (!await dbCreator.ExistsAsync(cancellationToken))
			{
				await dbCreator.CreateAsync(cancellationToken);
			}
		});
	}

	static async Task RunMigrationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
	{
		IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
		await strategy.ExecuteAsync(async () =>
		{
			// Run migration in a transaction to avoid partial migration if it fails.
			//await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
			await dbContext.Database.MigrateAsync(cancellationToken);
			//await transaction.CommitAsync(cancellationToken);
		});
	}

	static async Task SeedDataAsync(ApplicationDbContext dbContext, IPasswordHasher passwordHasher, CancellationToken cancellationToken)
	{
		// Role(s)
		string[] roles = ["Owner", "Administrator", "Editor", "ReadOnly"];
		RoleStore<IdentityRole> roleStore = new(dbContext);
		// User(s)
		AppUser[] users = [
			new()
			{
				Id = "436C6561-6E41-7263-6869-746563747572",
				UserName = "JoeBloggs",
				NormalizedUserName = "joebloggs",
				Email = "Joe@Bloggs.com",
				NormalizedEmail = "joe@bloggs.com",
				PhoneNumber = "1234567890",
				PhoneNumberConfirmed = true,
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
				PasswordHash = passwordHasher.Hash("JoeBloggs1!")
			}
			];
		UserStore<IdentityUser> userStore = new(dbContext);

		IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
		await strategy.ExecuteAsync(async () =>
		{
			// Seed the database
			//await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
			foreach (var role in roles)
			{
				await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToLower() }, cancellationToken);
			}

			foreach (var user in users)
			{
				await userStore.CreateAsync(user, cancellationToken);
				await userStore.AddToRoleAsync(user, roles[0].ToLower(), cancellationToken);
			}

			await dbContext.SaveChangesAsync(cancellationToken);
			//await transaction.CommitAsync(cancellationToken);
		});
	}
}
