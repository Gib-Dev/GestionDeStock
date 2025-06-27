using Microsoft.EntityFrameworkCore;
using GESTION_DES_STOCKS.Models;
using GESTION_DES_STOCKS.Services;

namespace GESTION_DES_STOCKS.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(StockDbContext context, ILoggingService? loggingService = null)
        {
            try
            {
                loggingService?.LogInformation("Début de l'initialisation de la base de données");

                // Créer la base de données si elle n'existe pas
                await context.Database.EnsureCreatedAsync();
                loggingService?.LogInformation("Base de données créée avec succès");

                // Appliquer les migrations si nécessaire
                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.MigrateAsync();
                    loggingService?.LogInformation("Migrations appliquées avec succès");
                }

                // Vérifier si des données existent déjà
                if (!context.Users.Any())
                {
                    await SeedDataAsync(context, loggingService);
                    loggingService?.LogInformation("Données de base insérées avec succès");
                }
                else
                {
                    loggingService?.LogInformation("Base de données déjà initialisée");
                }
            }
            catch (Exception ex)
            {
                loggingService?.LogError("Erreur lors de l'initialisation de la base de données", ex);
                throw;
            }
        }

        private static async Task SeedDataAsync(StockDbContext context, ILoggingService? loggingService)
        {
            try
            {
                // Créer l'utilisateur administrateur par défaut
                var adminUser = new User
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
                };

                context.Users.Add(adminUser);
                loggingService?.LogInformation("Utilisateur administrateur créé");

                // Créer les catégories par défaut
                var categories = new List<Category>
                {
                    new Category { CategoryID = 1, CategoryName = "Électronique", Description = "Produits électroniques et informatiques", Color = "#FF5722", CreatedAt = DateTime.Now, IsActive = true },
                    new Category { CategoryID = 2, CategoryName = "Vêtements", Description = "Vêtements et accessoires de mode", Color = "#2196F3", CreatedAt = DateTime.Now, IsActive = true },
                    new Category { CategoryID = 3, CategoryName = "Livres", Description = "Livres, magazines et publications", Color = "#4CAF50", CreatedAt = DateTime.Now, IsActive = true },
                    new Category { CategoryID = 4, CategoryName = "Alimentation", Description = "Produits alimentaires et boissons", Color = "#FF9800", CreatedAt = DateTime.Now, IsActive = true },
                    new Category { CategoryID = 5, CategoryName = "Sport", Description = "Équipements et accessoires de sport", Color = "#9C27B0", CreatedAt = DateTime.Now, IsActive = true },
                    new Category { CategoryID = 6, CategoryName = "Maison", Description = "Articles pour la maison et le jardin", Color = "#795548", CreatedAt = DateTime.Now, IsActive = true }
                };

                context.Categories.AddRange(categories);
                loggingService?.LogInformation($"{categories.Count} catégories créées");

                // Créer des produits d'exemple
                var products = new List<Product>
                {
                    new Product 
                    { 
                        ProductID = 1, 
                        ProductName = "Ordinateur Portable Dell", 
                        Description = "Ordinateur portable Dell Inspiron 15 pouces, Intel i5, 8GB RAM, 256GB SSD", 
                        Price = 899.99m, 
                        StockQuantity = 25, 
                        MinimumStockLevel = 5, 
                        SKU = "LAP001", 
                        Barcode = "1234567890123",
                        CategoryID = 1, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    },
                    new Product 
                    { 
                        ProductID = 2, 
                        ProductName = "Smartphone Samsung Galaxy", 
                        Description = "Smartphone Samsung Galaxy S23, 128GB, Noir", 
                        Price = 599.99m, 
                        StockQuantity = 50, 
                        MinimumStockLevel = 10, 
                        SKU = "PHN001", 
                        Barcode = "1234567890124",
                        CategoryID = 1, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    },
                    new Product 
                    { 
                        ProductID = 3, 
                        ProductName = "T-shirt Coton Premium", 
                        Description = "T-shirt en coton bio, taille M, couleurs disponibles", 
                        Price = 19.99m, 
                        StockQuantity = 100, 
                        MinimumStockLevel = 20, 
                        SKU = "TSH001", 
                        Barcode = "1234567890125",
                        CategoryID = 2, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    },
                    new Product 
                    { 
                        ProductID = 4, 
                        ProductName = "Livre Programmation C#", 
                        Description = "Guide complet de programmation C# pour débutants", 
                        Price = 29.99m, 
                        StockQuantity = 15, 
                        MinimumStockLevel = 5, 
                        SKU = "BK001", 
                        Barcode = "1234567890126",
                        CategoryID = 3, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    },
                    new Product 
                    { 
                        ProductID = 5, 
                        ProductName = "Café Arabica Bio", 
                        Description = "Café arabica bio, 250g, torréfaction moyenne", 
                        Price = 8.99m, 
                        StockQuantity = 75, 
                        MinimumStockLevel = 15, 
                        SKU = "CF001", 
                        Barcode = "1234567890127",
                        CategoryID = 4, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    },
                    new Product 
                    { 
                        ProductID = 6, 
                        ProductName = "Ballon de Football", 
                        Description = "Ballon de football professionnel, taille 5", 
                        Price = 24.99m, 
                        StockQuantity = 30, 
                        MinimumStockLevel = 8, 
                        SKU = "SP001", 
                        Barcode = "1234567890128",
                        CategoryID = 5, 
                        CreatedAt = DateTime.Now, 
                        IsActive = true 
                    }
                };

                context.Products.AddRange(products);
                loggingService?.LogInformation($"{products.Count} produits créés");

                // Créer quelques mouvements de stock d'exemple
                var movements = new List<StockMovement>
                {
                    new StockMovement
                    {
                        MovementID = 1,
                        ProductID = 1,
                        Type = MovementType.In,
                        Quantity = 30,
                        Reason = "Stock initial",
                        MovementDate = DateTime.Now.AddDays(-7),
                        UnitPrice = 899.99m
                    },
                    new StockMovement
                    {
                        MovementID = 2,
                        ProductID = 1,
                        Type = MovementType.Out,
                        Quantity = 5,
                        Reason = "Vente",
                        MovementDate = DateTime.Now.AddDays(-3),
                        UnitPrice = 899.99m
                    },
                    new StockMovement
                    {
                        MovementID = 3,
                        ProductID = 2,
                        Type = MovementType.In,
                        Quantity = 50,
                        Reason = "Réapprovisionnement",
                        MovementDate = DateTime.Now.AddDays(-5),
                        UnitPrice = 599.99m
                    }
                };

                context.StockMovements.AddRange(movements);
                loggingService?.LogInformation($"{movements.Count} mouvements de stock créés");

                // Sauvegarder toutes les modifications
                await context.SaveChangesAsync();
                loggingService?.LogInformation("Données de base sauvegardées avec succès");
            }
            catch (Exception ex)
            {
                loggingService?.LogError("Erreur lors de l'insertion des données de base", ex);
                throw;
            }
        }

        public static async Task BackupDatabaseAsync(string backupPath)
        {
            try
            {
                var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StockManagement.db");
                if (File.Exists(sourcePath))
                {
                    var backupFileName = $"StockManagement_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                    var fullBackupPath = Path.Combine(backupPath, backupFileName);
                    
                    Directory.CreateDirectory(backupPath);
                    File.Copy(sourcePath, fullBackupPath, true);
                    
                    // Garder seulement les 10 derniers backups
                    var backupFiles = Directory.GetFiles(backupPath, "StockManagement_backup_*.db")
                        .OrderByDescending(f => f)
                        .Skip(10);
                    
                    foreach (var oldBackup in backupFiles)
                    {
                        File.Delete(oldBackup);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde: {ex.Message}");
            }
        }
    }
} 