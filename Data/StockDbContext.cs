using Microsoft.EntityFrameworkCore;
using GESTION_DES_STOCKS.Models;

namespace GESTION_DES_STOCKS.Data
{
    public class StockDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=StockManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Product)
                .WithMany()
                .HasForeignKey(sm => sm.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.User)
                .WithMany()
                .HasForeignKey(sm => sm.UserID)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuration des index
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Barcode)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Données de base
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Utilisateur admin par défaut (mot de passe: admin123)
            modelBuilder.Entity<User>().HasData(new User
            {
                UserID = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Email = "admin@stockges.com",
                FirstName = "Administrateur",
                LastName = "Système",
                Role = UserRole.Admin,
                CreatedAt = DateTime.Now,
                IsActive = true
            });

            // Catégories par défaut
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Électronique", Description = "Produits électroniques", Color = "#FF5722", CreatedAt = DateTime.Now, IsActive = true },
                new Category { CategoryID = 2, CategoryName = "Vêtements", Description = "Vêtements et accessoires", Color = "#2196F3", CreatedAt = DateTime.Now, IsActive = true },
                new Category { CategoryID = 3, CategoryName = "Livres", Description = "Livres et publications", Color = "#4CAF50", CreatedAt = DateTime.Now, IsActive = true },
                new Category { CategoryID = 4, CategoryName = "Alimentation", Description = "Produits alimentaires", Color = "#FF9800", CreatedAt = DateTime.Now, IsActive = true }
            );

            // Produits par défaut
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "Ordinateur Portable", Description = "Ordinateur portable 15 pouces", Price = 899.99m, StockQuantity = 25, MinimumStockLevel = 5, SKU = "LAP001", CategoryID = 1, CreatedAt = DateTime.Now, IsActive = true },
                new Product { ProductID = 2, ProductName = "Smartphone", Description = "Smartphone dernière génération", Price = 599.99m, StockQuantity = 50, MinimumStockLevel = 10, SKU = "PHN001", CategoryID = 1, CreatedAt = DateTime.Now, IsActive = true },
                new Product { ProductID = 3, ProductName = "T-shirt", Description = "T-shirt en coton", Price = 19.99m, StockQuantity = 100, MinimumStockLevel = 20, SKU = "TSH001", CategoryID = 2, CreatedAt = DateTime.Now, IsActive = true }
            );
        }
    }
} 