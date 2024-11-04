namespace Infrastructure.Users;

sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		_ = builder.HasKey(u => u.Id);

		_ = builder.HasIndex(u => u.Email).IsUnique();
	}
}