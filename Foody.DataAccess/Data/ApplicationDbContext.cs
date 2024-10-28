﻿using Microsoft.EntityFrameworkCore;
using Foody.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Foody .Models;
namespace Foody.DataAccess.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
             
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Tech Solution",
                    StreetAddress = "123 Tech St",
                    City = "Tech City",
                    PhoneNumber = "6669990000"
                },
                new Company
                {
                    Id = 2,
                    Name = "Vivid Books",
                    StreetAddress = "999 Vid St",
                    City = "Vid City",
                    PhoneNumber = "7779990000"
                },
                new Company
                {
                    Id = 3,
                    Name = "Readers Club",
                    StreetAddress = "999 Main St",
                    City = "Lala land",
                    PhoneNumber      = "1113335555"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Drinks", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Foods", DisplayOrder = 2 }
                );
          


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Prime",
                    Description = "",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    imageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Name = "Coca Cola",
                    Description = "",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    imageUrl = ""
                }

                );
        }
    }
}
