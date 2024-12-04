﻿// Ignore Spelling: Todo

namespace Infrastructure.Database;

/// <summary>Application database context.</summary>
/// <param name="options">Context options.</param>
/// <param name="publisher">Publisher.</param>
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : DbContext(options), IApplicationDbContext
{
	/// <summary>Users.</summary>
	public DbSet<User> Users { get; set; } = null!;

	/// <summary>To do items</summary>
	public DbSet<TodoItem> TodoItems { get; set; } = null!;

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);
		_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		_ = modelBuilder.HasDefaultSchema(Schemas.Default);
	}

	/// <inheritdoc/>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		// When should you publish domain events?
		//
		// 1. BEFORE calling SaveChangesAsync
		//     - domain events are part of the same transaction
		//     - immediate consistency
		// 2. AFTER calling SaveChangesAsync
		//     - domain events are a separate transaction
		//     - eventual consistency
		//     - handlers can fail

		int result = await base.SaveChangesAsync(cancellationToken);

		await PublishDomainEventsAsync();

		return result;
	}

	async Task PublishDomainEventsAsync()
	{
		List<IDomainEvent>? domainEvents = ChangeTracker
			.Entries<Entity>()
			.Select(entry => entry.Entity)
			.SelectMany(entity =>
			{
				List<IDomainEvent>? domainEvents = [];
				if (entity is not null)
				{
					if (entity.DomainEvents is not null && entity.DomainEvents.Count != 0)
					{
						domainEvents = new List<IDomainEvent>(entity.DomainEvents);
					}

					entity.ClearDomainEvents();
				}

				return domainEvents!;
			})
			.ToList();

		foreach (IDomainEvent domainEvent in domainEvents)
		{
			await publisher.Publish(domainEvent);
		}
	}
}