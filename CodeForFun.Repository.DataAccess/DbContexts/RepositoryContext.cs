﻿using CodeForFun.Repository.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;

using Microsoft.Extensions.Options;
using CodeForFun.UI.WebMvcCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeForFun.Repository.DataAccess.DbContexts
{
	public partial class RepositoryContext : DbContext
	{

		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
		{
		}
		public RepositoryContext()
		{
			Database.EnsureCreated();
			this.Roles.Add(new Role { Name = "Admin" });
			this.Roles.Add(new Role { Name = "Member" });
			this.Roles.Add(new Role { Name = "User" });
			this.SaveChanges();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=code-for-fun-db;Trusted_Connection=True");

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductDetail> ProductDetails { get; set; }
		public DbSet<ProductsToCustomer> ProductsToCustomers { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>(entity =>
			{
				entity.Property(e => e.Name).IsUnicode(false);

				entity.HasOne(d => d.Parent)
					.WithMany(p => p.InverseParent)
					.HasForeignKey(d => d.ParentId)
					.HasConstraintName("FK_Categories_Categories");
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.Property(e => e.Name).IsUnicode(false);

				entity.Property(e => e.Surname).IsUnicode(false);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity
				.HasOne(x => x.Role)
				.WithMany(x => x.Users)
				.HasForeignKey(x => x.RoleId);

			});


			modelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.Code).IsUnicode(false);

				entity.Property(e => e.Name).IsUnicode(false);


				entity.HasOne(d => d.Category)
						.WithMany(p => p.Products)
						.HasForeignKey(d => d.CategoryId)
						.IsRequired(false);

				entity.HasOne(x => x.ProductDetail)
				.WithOne(x => x.IdNavigation)
				.HasForeignKey<ProductDetail>(x => x.Id)
								.IsRequired(false);
			});

			modelBuilder.Entity<ProductDetail>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Description).IsUnicode(false);

				entity.HasOne(d => d.IdNavigation)
							.WithOne(p => p.ProductDetail)
							.HasForeignKey<ProductDetail>(d => d.Id)
							.IsRequired(false);
			});

			modelBuilder.Entity<ProductsToCustomer>(entity =>
			{

				entity.HasOne(d => d.Customer)
					.WithMany(p => p.ProductsToCustomers)
					.HasForeignKey(d => d.CustomerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProductsToCustomers_Customers");

				entity.HasOne(d => d.Product)
									.WithMany(p => p.ProductsToCustomers)
									.HasForeignKey(d => d.ProductId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("FK_ProductsToCustomers_Products");
			});


			modelBuilder.Entity<Role>().HasData(new[]{
   new Role {
	  RoleID = 1, // Must be != 0
      Name = "Admin",
   },
   new Role {
	  RoleID = 2, // Must be != 0
      Name = "User",
   }
});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	}
}
