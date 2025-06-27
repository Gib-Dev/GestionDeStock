using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GESTION_DES_STOCKS.Models
{
    public class Rapport
    {
        public int RapportID { get; set; }
        public string RapportName { get; set; } = string.Empty;  // Initialisation pour éviter null
        public string Description { get; set; } = string.Empty;  // Initialisation pour éviter null
        public int Quantity { get; set; }
    }
}

