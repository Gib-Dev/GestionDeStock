using GESTION_DES_STOCKS.Data;
using GESTION_DES_STOCKS.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GESTION_DES_STOCKS.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductBySkuAsync(string sku);
        Task<List<Product>> SearchProductsAsync(string searchTerm);
        Task<List<Product>> GetLowStockProductsAsync();
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateStockAsync(int productId, int quantity, MovementType type, string? reason = null);
        Task<decimal> GetTotalStockValueAsync();
        Task<int> GetTotalProductsCountAsync();
    }

    public class ProductService : IProductService
    {
        private readonly StockDbContext _context;

        public ProductService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.ProductName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des produits: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductID == id && p.IsActive);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération du produit {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<Product?> GetProductBySkuAsync(string sku)
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.SKU == sku && p.IsActive);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération du produit par SKU {sku}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllProductsAsync();

                var term = searchTerm.ToLower();
                return await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive && 
                               (p.ProductName.ToLower().Contains(term) ||
                                p.Description.ToLower().Contains(term) ||
                                p.SKU.ToLower().Contains(term) ||
                                p.Barcode.ToLower().Contains(term)))
                    .OrderBy(p => p.ProductName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la recherche: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<List<Product>> GetLowStockProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive && p.StockQuantity <= p.MinimumStockLevel)
                    .OrderBy(p => p.StockQuantity)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des produits en stock faible: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                // Validation
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(product);
                
                if (!Validator.TryValidateObject(product, validationContext, validationResults, true))
                {
                    foreach (var error in validationResults)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                    }
                    return false;
                }

                // Vérifier si le SKU existe déjà
                if (!string.IsNullOrEmpty(product.SKU))
                {
                    var existingProduct = await GetProductBySkuAsync(product.SKU);
                    if (existingProduct != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Le SKU {product.SKU} existe déjà");
                        return false;
                    }
                }

                product.CreatedAt = DateTime.Now;
                product.IsActive = true;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de l'ajout du produit: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await GetProductByIdAsync(product.ProductID);
                if (existingProduct == null)
                    return false;

                // Validation
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(product);
                
                if (!Validator.TryValidateObject(product, validationContext, validationResults, true))
                {
                    foreach (var error in validationResults)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                    }
                    return false;
                }

                // Vérifier si le SKU existe déjà (sauf pour ce produit)
                if (!string.IsNullOrEmpty(product.SKU))
                {
                    var existingProductWithSku = await _context.Products
                        .FirstOrDefaultAsync(p => p.SKU == product.SKU && p.ProductID != product.ProductID && p.IsActive);
                    if (existingProductWithSku != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Le SKU {product.SKU} existe déjà");
                        return false;
                    }
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.MinimumStockLevel = product.MinimumStockLevel;
                existingProduct.SKU = product.SKU;
                existingProduct.Barcode = product.Barcode;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la mise à jour du produit: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await GetProductByIdAsync(id);
                if (product == null)
                    return false;

                // Soft delete
                product.IsActive = false;
                product.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la suppression du produit: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity, MovementType type, string? reason = null)
        {
            try
            {
                var product = await GetProductByIdAsync(productId);
                if (product == null)
                    return false;

                // Calculer la nouvelle quantité
                int newQuantity = type switch
                {
                    MovementType.In => product.StockQuantity + quantity,
                    MovementType.Out => product.StockQuantity - quantity,
                    MovementType.Adjustment => quantity,
                    MovementType.Return => product.StockQuantity + quantity,
                    _ => product.StockQuantity
                };

                if (newQuantity < 0)
                {
                    System.Diagnostics.Debug.WriteLine("Quantité insuffisante en stock");
                    return false;
                }

                // Mettre à jour le stock
                product.StockQuantity = newQuantity;
                product.UpdatedAt = DateTime.Now;

                // Enregistrer le mouvement
                var movement = new StockMovement
                {
                    ProductID = productId,
                    Type = type,
                    Quantity = quantity,
                    Reason = reason,
                    MovementDate = DateTime.Now,
                    UnitPrice = product.Price
                };

                _context.StockMovements.Add(movement);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la mise à jour du stock: {ex.Message}");
                return false;
            }
        }

        public async Task<decimal> GetTotalStockValueAsync()
        {
            try
            {
                return await _context.Products
                    .Where(p => p.IsActive)
                    .SumAsync(p => p.Price * p.StockQuantity);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du calcul de la valeur totale: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> GetTotalProductsCountAsync()
        {
            try
            {
                return await _context.Products
                    .Where(p => p.IsActive)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du comptage des produits: {ex.Message}");
                return 0;
            }
        }
    }
} 