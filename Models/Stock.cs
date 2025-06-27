using System;

namespace GESTION_DES_STOCKS.Models
{
    public class Stock
    {
        private static int _nextStockId = 1;

        // Rendre le setter accessible dans le même projet (internal set)
        public int StockID { get; set; }

        public string StockName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public Stock()
        {
            // Génère automatiquement un ID si non précisé
            StockID = _nextStockId++;
        }
    }
}
