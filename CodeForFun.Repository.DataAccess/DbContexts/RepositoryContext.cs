﻿using CodeForFun.Repository.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;

using Microsoft.Extensions.Options;
using CodeForFun.UI.WebMvcCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace CodeForFun.Repository.DataAccess.DbContexts
{
	public partial class RepositoryContext : DbContext
	{
		private readonly IWebHostEnvironment _env;
		IConfiguration _iconfiguration;
		public RepositoryContext(DbContextOptions<RepositoryContext> options, IWebHostEnvironment env, IConfiguration iconfiguration) : base(options)
		{
			_env = env;
			_iconfiguration = iconfiguration;

		}
		public RepositoryContext()
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(_env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			IConfigurationRoot configurationRoot = configurationBuilder.Build();
			var conn = configurationRoot.GetConnectionString("DefaultConnection");
			//string connString = Startup.StaticConfig.GetConnectionString("DefaultConnection");

			optionsBuilder.UseSqlServer(conn);
		}
		public DbSet<User> Users { get; set; }
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

			modelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.Code).IsUnicode(false);

				entity.Property(e => e.Name).IsUnicode(false);


				entity.HasOne(d => d.Category)
						.WithMany(p => p.Products)
						.HasForeignKey(d => d.CategoryId)
						.HasConstraintName("FK_Products_Categories");

				entity.HasOne(x => x.ProductDetail)
				.WithOne(x => x.IdNavigation)
				.HasForeignKey<ProductDetail>(x => x.Id);
			});

			modelBuilder.Entity<ProductDetail>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Description).IsUnicode(false);

				entity.HasOne(d => d.IdNavigation)
							.WithOne(p => p.ProductDetail)
							.HasForeignKey<ProductDetail>(d => d.Id)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("FK_ProductDetails_Products");
			});

			modelBuilder.Entity<ProductsToCustomer>(entity =>
			{
				entity.HasKey(e => new
				{
					e.CustomerId,
					e.ProductId
				});

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

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	}
}
