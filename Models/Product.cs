using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GESTION_DES_STOCKS.Models
{
    public class Product
    {
        private static int _nextProductId = 1;

        public int ProductID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix doit être positif")]
        public decimal Price { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité doit être positive")]
        public int StockQuantity { get; set; }
        
        [Range(0, int.MaxValue)]
        public int MinimumStockLevel { get; set; } = 10;
        
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;
        
        public int CategoryID { get; set; }
        public virtual Category? Category { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Propriété calculée pour vérifier si le stock est faible
        public bool IsLowStock => StockQuantity <= MinimumStockLevel;
        
        // Propriété calculée pour la valeur totale du stock
        public decimal TotalStockValue => Price * StockQuantity;

        public Product()
        {
            ProductID = _nextProductId++; // Affecte l'ID actuel et incrémente pour le prochain
        }
    }
}
