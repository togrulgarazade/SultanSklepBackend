using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SultanSklepBackend.Models;

namespace SultanSklepBackend.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<ContactAdmin> ContactAdmin { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProductOperations> ProductOperations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(u => u.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_AppUser_Email");

                entity.Property(u => u.CreateDT)
                    .HasDefaultValueSql("DATEADD(HOUR, 1, GETUTCDATE())");

                entity.HasIndex(u => u.PhoneNumber)
                    .IsUnique()
                    .HasDatabaseName("IX_AppUser_PhoneNumber");
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(255); 
                entity.Property(c => c.CreateDT)
                    .HasDefaultValueSql("DATEADD(HOUR, 1, GETUTCDATE())");
                entity.Property(c => c.IsDeleted)
                    .HasDefaultValue(false); 
            });


            modelBuilder.Entity<ContactAdmin>(entity =>
            {
                entity.HasKey(ca => ca.Id); 
                entity.Property(ca => ca.Email)
                    .IsRequired()
                    .HasMaxLength(255); 
                entity.Property(ca => ca.Message)
                    .IsRequired(); 
                entity.Property(ca => ca.CreateDT)
                    .HasDefaultValueSql("DATEADD(HOUR, 1, GETUTCDATE())");
                entity.Property(ca => ca.Fullname)
                    .IsRequired()
                    .HasMaxLength(255);
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id); 
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(p => p.CreateDT)
                    .HasDefaultValueSql("DATEADD(HOUR, 1, GETUTCDATE())");
                entity.Property(p => p.IsDeleted)
                    .HasDefaultValue(false); 
                entity.Property(p => p.IsInStock)
                    .HasDefaultValue(true); 
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)"); 
                entity.Property(p => p.Description)
                    .HasMaxLength(1000); 
            });
        }

    }
}
