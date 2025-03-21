using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;


namespace Persistence
{
	public class RepositoryDbContext : DbContext
	{

		public RepositoryDbContext(DbContextOptions options)
		: base(options)
		{
		}

		public DbSet<Product> Products { get; init; }
		public DbSet<Cart> Carts { get; init; }
		public DbSet<Order> Orders { get; init; }
		public DbSet<Payment> Payments { get; init; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>().ToCollection("Products");
			modelBuilder.Entity<Cart>().ToCollection("Carts");
			modelBuilder.Entity<Order>().ToCollection("Orders");
			modelBuilder.Entity<Payment>().ToCollection("Payments");
		}
	}
}
