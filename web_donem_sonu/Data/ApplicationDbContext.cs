using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
			.HasIndex(o => o.Email)
			.IsUnique();

			modelBuilder.Entity<User>()
			.HasOne(o => o.Role)
			.WithMany(r => r.Users)
			.HasForeignKey(u => u.RoleId);

			modelBuilder.Entity<Product>()
	        .HasOne(p => p.Creator)
	        .WithMany() 
	        .HasForeignKey(p => p.CreatedBy);

			modelBuilder.Entity<Order>()
			   .HasOne(o => o.User)
			   .WithMany(u => u.Orders)
			   .HasForeignKey(o => o.UserId);

			modelBuilder.Entity<OrderItem>()
			   .HasOne(oi => oi.Order)
			   .WithMany(o => o.OrderItems)
			   .HasForeignKey(oi => oi.OrderId);

			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Product)
				.WithMany()
				.HasForeignKey(oi => oi.ProductId);
		}
	}
}
