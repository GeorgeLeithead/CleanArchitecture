﻿// Ignore Spelling: dbo

// <auto-generated />
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Infrastructure.Database.Migrations
{
	[DbContext(typeof(ApplicationDbContext))]
	partial class ApplicationDbContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasDefaultSchema("dbo")
				.HasAnnotation("ProductVersion", "8.0.7")
				.HasAnnotation("Relational:MaxIdentifierLength", 63);

			SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

			modelBuilder.Entity("Domain.Todos.TodoItem", b =>
				{
					b.Property<Guid>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("UNIQUEIDENTIFIER")
						.HasColumnName("id");

					b.Property<DateTime?>("CompletedAt")
						.HasColumnType("DATETIMEOFFSET")
						.HasColumnName("completed_at");

					b.Property<DateTime>("CreatedAt")
						.HasColumnType("DATETIMEOFFSET")
						.HasColumnName("created_at");

					b.Property<string>("Description")
						.IsRequired()
						.HasColumnType("NVARCHAR(MAX)")
						.HasColumnName("description");

					b.Property<DateTime?>("DueDate")
						.HasColumnType("DATETIMEOFFSET")
						.HasColumnName("due_date");

					b.Property<bool>("IsCompleted")
						.HasColumnType("BIT")
						.HasColumnName("is_completed");

					b.Property<List<string>>("Labels")
						.IsRequired()
						.HasColumnType("NVARCHAR(MAX)")
						.HasColumnName("labels");

					b.Property<int>("Priority")
						.HasColumnType("SMALLINT")
						.HasColumnName("priority");

					b.Property<Guid>("UserId")
						.HasColumnType("UNIQUEIDENTIFIER")
						.HasColumnName("user_id");

					b.HasKey("Id")
						.HasName("pk_todo_items");

					b.HasIndex("UserId")
						.HasDatabaseName("ix_todo_items_user_id");

					b.ToTable("todo_items", "dbo");
				});

			modelBuilder.Entity("Domain.Users.User", b =>
				{
					b.Property<Guid>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("UNIQUEIDENTIFIER")
						.HasColumnName("id");

					b.Property<string>("Email")
						.IsRequired()
						.HasColumnType("VARCHAR(320)")
						.HasColumnName("email");

					b.Property<string>("FirstName")
						.IsRequired()
						.HasColumnType("NVARCHAR(255)")
						.HasColumnName("first_name");

					b.Property<string>("LastName")
						.IsRequired()
						.HasColumnType("NVARCHAR(255)")
						.HasColumnName("last_name");

					b.Property<string>("PasswordHash")
						.IsRequired()
						.HasColumnType("NVARCHAR(MAX)")
						.HasColumnName("password_hash");

					b.HasKey("Id")
						.HasName("pk_users");

					b.HasIndex("Email")
						.IsUnique()
						.HasDatabaseName("ix_users_email");

					b.ToTable("users", "dbo");
				});

			modelBuilder.Entity("Domain.Todos.TodoItem", b =>
				{
					b.HasOne("Domain.Users.User", null)
						.WithMany()
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired()
						.HasConstraintName("fk_todo_items_users_user_id");
				});
#pragma warning restore 612, 618
		}
	}
}
