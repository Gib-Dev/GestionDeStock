using System.ComponentModel.DataAnnotations;

namespace GESTION_DES_STOCKS.Models
{
    public class StockMovement
    {
        public int MovementID { get; set; }
        
        public int ProductID { get; set; }
        public virtual Product? Product { get; set; }
        
        [Required]
        public MovementType Type { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être positive")]
        public int Quantity { get; set; }
        
        [StringLength(500)]
        public string? Reason { get; set; }
        
        public int? UserID { get; set; }
        public virtual User? User { get; set; }
        
        public DateTime MovementDate { get; set; } = DateTime.Now;
        
        public decimal? UnitPrice { get; set; }
        
        public decimal TotalValue => (UnitPrice ?? 0) * Quantity;
        
        [StringLength(100)]
        public string? Reference { get; set; }
    }
    
    public enum MovementType
    {
        In,     // Entrée en stock
        Out,    // Sortie de stock
        Adjustment, // Ajustement
        Return, // Retour
        Transfer // Transfert
    }
} 