namespace Infrastructure.Todos;

sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
	public void Configure(EntityTypeBuilder<TodoItem> builder)
	{
		_ = builder.HasKey(t => t.Id);

		_ = builder.Property(t => t.DueDate).HasConversion(
				v => v.HasValue ? new DateTimeOffset(v.Value) : (DateTimeOffset?)null, // Convert DateTime? to DateTimeOffset? when saving to the database
				v => v.HasValue ? v.Value.LocalDateTime : null // Convert DateTimeOffset? to local DateTime? when reading from the database
			);

		_ = builder.Property(t => t.CreatedAt).HasConversion(
				v => new DateTimeOffset(v), // Convert DateTime to DateTimeOffset when saving to the database
				v => v.LocalDateTime // Convert DateTimeOffset to local DateTime when reading from the database
			);

		_ = builder.Property(t => t.DueDate).HasConversion(
				v => v.HasValue ? new DateTimeOffset(v.Value) : (DateTimeOffset?)null, // Convert DateTime? to DateTimeOffset? when saving to the database
				v => v.HasValue ? v.Value.LocalDateTime : null // Convert DateTimeOffset? to local DateTime? when reading from the database
			);

		_ = builder.Property(t => t.CompletedAt).HasConversion(
				v => v.HasValue ? new DateTimeOffset(v.Value) : (DateTimeOffset?)null, // Convert DateTime? to DateTimeOffset? when saving to the database
				v => v.HasValue ? v.Value.LocalDateTime : null // Convert DateTimeOffset? to local DateTime? when reading from the database
			);

		_ = builder.Property(t => t.Priority).HasConversion(
				v => (short)v, // Convert Int32 to Int16 when saving to the database
				v => (Priority)v // Convert Int16 to Int32 when reading from the database
			);

		_ = builder.Property(t => t.Labels).HasConversion<CommaSplitStringConverter, SplitStringComparer>();

		_ = builder.HasOne<User>().WithMany().HasForeignKey(t => t.UserId);
	}
}
