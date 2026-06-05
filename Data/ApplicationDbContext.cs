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
        public DbSet<Peminjaman> Peminjamans { get; set; }
        public DbSet<RiwayatAset> RiwayatAsets { get; set; }

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

            builder.Entity<Asset>().HasData(
                new Asset { Id = 1, AssetCode = "AST-001", Name = "Laptop Dell Inspiron 15", CategoryId = 1, LocationId = 1, Status = AssetStatus.Tersedia, PurchasePrice = 9500000, PurchaseDate = new DateTime(2023, 1, 10), CreatedAt = new DateTime(2023, 1, 10) },
                new Asset { Id = 2, AssetCode = "AST-002", Name = "Laptop Lenovo ThinkPad", CategoryId = 1, LocationId = 1, Status = AssetStatus.Digunakan, PurchasePrice = 12000000, PurchaseDate = new DateTime(2023, 2, 5), CreatedAt = new DateTime(2023, 2, 5) },
                new Asset { Id = 3, AssetCode = "AST-003", Name = "Printer Canon G2020", CategoryId = 1, LocationId = 2, Status = AssetStatus.Tersedia, PurchasePrice = 2300000, PurchaseDate = new DateTime(2023, 3, 1), CreatedAt = new DateTime(2023, 3, 1) },
                new Asset { Id = 4, AssetCode = "AST-004", Name = "Meja Kerja Kayu", CategoryId = 2, LocationId = 1, Status = AssetStatus.Tersedia, PurchasePrice = 1500000, PurchaseDate = new DateTime(2022, 6, 15), CreatedAt = new DateTime(2022, 6, 15) },
                new Asset { Id = 5, AssetCode = "AST-005", Name = "Kursi Ergonomis", CategoryId = 2, LocationId = 1, Status = AssetStatus.Tersedia, PurchasePrice = 2800000, PurchaseDate = new DateTime(2022, 7, 20), CreatedAt = new DateTime(2022, 7, 20) },
                new Asset { Id = 6, AssetCode = "AST-006", Name = "Lemari Arsip", CategoryId = 2, LocationId = 2, Status = AssetStatus.Tersedia, PurchasePrice = 1200000, PurchaseDate = new DateTime(2022, 8, 10), CreatedAt = new DateTime(2022, 8, 10) },
                new Asset { Id = 7, AssetCode = "AST-007", Name = "Motor Honda Vario", CategoryId = 3, LocationId = 1, Status = AssetStatus.Tersedia, PurchasePrice = 21000000, PurchaseDate = new DateTime(2021, 4, 1), CreatedAt = new DateTime(2021, 4, 1) },
                new Asset { Id = 8, AssetCode = "AST-008", Name = "Mobil Toyota Avanza", CategoryId = 3, LocationId = 2, Status = AssetStatus.Perbaikan, PurchasePrice = 185000000, PurchaseDate = new DateTime(2020, 1, 15), CreatedAt = new DateTime(2020, 1, 15) },
                new Asset { Id = 9, AssetCode = "AST-009", Name = "Monitor LG 24 inch", CategoryId = 1, LocationId = 1, Status = AssetStatus.Tersedia, PurchasePrice = 3200000, PurchaseDate = new DateTime(2023, 5, 10), CreatedAt = new DateTime(2023, 5, 10) },
                new Asset { Id = 10, AssetCode = "AST-010", Name = "Proyektor Epson", CategoryId = 1, LocationId = 2, Status = AssetStatus.Tersedia, PurchasePrice = 7500000, PurchaseDate = new DateTime(2022, 11, 3), CreatedAt = new DateTime(2022, 11, 3) }
            );
        }
    }
}