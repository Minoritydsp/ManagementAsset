using ManagementAsset.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementAsset.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronik", Description = "Laptop, printer, dll" },
                new Category { Id = 2, Name = "Furniture", Description = "Meja, kursi, lemari" },
                new Category { Id = 3, Name = "Kendaraan", Description = "Motor, mobil" }
            );

            builder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "kantor pusat", Address = "Jakarta" },
                new Location { Id = 2, Name = "Gudang", Address = "Bandung" }
            );
        }
    }
}