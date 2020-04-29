﻿// <auto-generated />
using System;
using CodeForFun.Repository.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeForFun.Repository.DataAccess.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Category", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<short?>("ParentId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(24)")
                        .HasMaxLength(24)
                        .IsUnicode(false);

                    b.Property<string>("Surname")
                        .HasColumnType("varchar(24)")
                        .HasMaxLength(24)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CategoryId")
                        .HasColumnType("smallint");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(16)")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.Property<DateTime>("DateRegister")
                        .HasColumnName("Date_Register")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(9, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.ProductsToCustomer", b =>
                {
                    b.Property<int>("ProductsToCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductsToCustomerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsToCustomers");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("CodeForFun.UI.WebMvcCore.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Category", b =>
                {
                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Category", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_Categories_Categories");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.Product", b =>
                {
                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Products_Categories");
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.ProductDetail", b =>
                {
                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Product", "IdNavigation")
                        .WithOne("ProductDetail")
                        .HasForeignKey("CodeForFun.Repository.Entities.Concrete.ProductDetail", "Id")
                        .HasConstraintName("FK_ProductDetails_Products")
                        .IsRequired();
                });

            modelBuilder.Entity("CodeForFun.Repository.Entities.Concrete.ProductsToCustomer", b =>
                {
                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Customer", "Customer")
                        .WithMany("ProductsToCustomers")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_ProductsToCustomers_Customers")
                        .IsRequired();

                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Product", "Product")
                        .WithMany("ProductsToCustomers")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductsToCustomers_Products")
                        .IsRequired();
                });

            modelBuilder.Entity("CodeForFun.UI.WebMvcCore.Models.User", b =>
                {
                    b.HasOne("CodeForFun.Repository.Entities.Concrete.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
