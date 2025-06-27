using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GESTION_DES_STOCKS.Models
{
    public class Category
    {
        private static int _nextCategoryId = 1;

        // Rendez CategoryID accessible en lecture et en écriture
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Color { get; set; } = "#6A00F4";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation property pour les produits
        public virtual ICollection<Product>? Products { get; set; }
        
        // Propriété calculée pour le nombre de produits
        public int ProductCount => Products?.Count ?? 0;

        public Category()
        {
            // Initialise CategoryID avec un ID unique
            CategoryID = _nextCategoryId++;
        }
    }
}
