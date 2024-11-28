using Microsoft.EntityFrameworkCore;
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
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



       
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Drinks" },
                new Category { Id = 2, Name = "Foods" },
                new Category { Id = 3, Name = "Addition Foods" },
                new Category { Id = 4, Name = "Others" }

                );
          


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Prime",
                    Description = "",
                    ListPrice = 99,
                    Price = 90,
                CategoryId = 1,
                    imageUrl = ""
                }

                );
        }
    }
}
