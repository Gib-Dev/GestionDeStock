using GESTION_DES_STOCKS.Data;
using GESTION_DES_STOCKS.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GESTION_DES_STOCKS.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetActiveCategoriesAsync();
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<int> GetCategoryProductCountAsync(int categoryId);
        Task<decimal> GetCategoryTotalValueAsync(int categoryId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly StockDbContext _context;

        public CategoryService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.Products)
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des catégories: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.CategoryID == id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération de la catégorie {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.Products)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des catégories actives: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                // Validation
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(category);
                
                if (!Validator.TryValidateObject(category, validationContext, validationResults, true))
                {
                    foreach (var error in validationResults)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                    }
                    return false;
                }

                // Vérifier si la catégorie existe déjà
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == category.CategoryName.ToLower() && c.IsActive);

                if (existingCategory != null)
                {
                    System.Diagnostics.Debug.WriteLine($"La catégorie {category.CategoryName} existe déjà");
                    return false;
                }

                category.CreatedAt = DateTime.Now;
                category.IsActive = true;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de l'ajout de la catégorie: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                var existingCategory = await GetCategoryByIdAsync(category.CategoryID);
                if (existingCategory == null)
                    return false;

                // Validation
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(category);
                
                if (!Validator.TryValidateObject(category, validationContext, validationResults, true))
                {
                    foreach (var error in validationResults)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                    }
                    return false;
                }

                // Vérifier si le nom existe déjà (sauf pour cette catégorie)
                var existingCategoryWithName = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == category.CategoryName.ToLower() 
                                             && c.CategoryID != category.CategoryID 
                                             && c.IsActive);
                if (existingCategoryWithName != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Le nom de catégorie {category.CategoryName} existe déjà");
                    return false;
                }

                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                existingCategory.Color = category.Color;
                existingCategory.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la mise à jour de la catégorie: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await GetCategoryByIdAsync(id);
                if (category == null)
                    return false;

                // Vérifier s'il y a des produits dans cette catégorie
                if (category.Products?.Any() == true)
                {
                    System.Diagnostics.Debug.WriteLine("Impossible de supprimer une catégorie contenant des produits");
                    return false;
                }

                // Soft delete
                category.IsActive = false;
                category.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la suppression de la catégorie: {ex.Message}");
                return false;
            }
        }

        public async Task<int> GetCategoryProductCountAsync(int categoryId)
        {
            try
            {
                return await _context.Products
                    .Where(p => p.CategoryID == categoryId && p.IsActive)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du comptage des produits de la catégorie: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetCategoryTotalValueAsync(int categoryId)
        {
            try
            {
                return await _context.Products
                    .Where(p => p.CategoryID == categoryId && p.IsActive)
                    .SumAsync(p => p.Price * p.StockQuantity);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du calcul de la valeur totale de la catégorie: {ex.Message}");
                return 0;
            }
        }
    }
} 