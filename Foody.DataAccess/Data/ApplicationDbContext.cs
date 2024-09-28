using Microsoft.EntityFrameworkCore;
using Foody.Models;
namespace Foody.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
             
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Drinks", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Foods", DisplayOrder = 2 }
                );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Sting",
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
