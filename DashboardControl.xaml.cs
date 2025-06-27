using System.Windows;
using System.Windows.Controls;
using GESTION_DES_STOCKS.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GESTION_DES_STOCKS
{
    public partial class DashboardControl : UserControl
    {
        private readonly IProductService _productService;

        public DashboardControl(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productService = serviceProvider.GetRequiredService<IProductService>();
            
            LoadDashboardDataAsync();
        }

        private async void LoadDashboardDataAsync()
        {
            try
            {
                // Charger les données en arrière-plan
                var totalProductsTask = _productService.GetTotalProductsCountAsync();
                var totalStockValueTask = _productService.GetTotalStockValueAsync();
                var lowStockProductsTask = _productService.GetLowStockProductsAsync();

                // Attendre que toutes les tâches se terminent
                var totalProducts = await totalProductsTask;
                var totalStockValue = await totalStockValueTask;
                var lowStockProducts = await lowStockProductsTask;

                // Mettre à jour l'interface
                UpdateDashboardUI(totalProducts, totalStockValue, lowStockProducts.Count);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement du tableau de bord: {ex.Message}");
                // En cas d'erreur, afficher des valeurs par défaut
                UpdateDashboardUI(0, 0, 0);
            }
        }

        private void UpdateDashboardUI(int totalProducts, decimal totalStockValue, int lowStockCount)
        {
            // Mettre à jour les TextBlocks avec les données réelles
            // Note: Vous devrez ajouter des x:Name aux TextBlocks dans le XAML pour les référencer ici
            
            // Exemple de mise à jour (ajustez selon votre structure XAML)
            var textBlocks = this.FindChildren<TextBlock>();
            foreach (var textBlock in textBlocks)
            {
                if (textBlock.Text.Contains("340 Produits"))
                {
                    textBlock.Text = $"{totalProducts} Produits";
                }
                else if (textBlock.Text.Contains("1245 Articles"))
                {
                    textBlock.Text = $"{totalProducts} Articles";
                }
                else if (textBlock.Text.Contains("3 Nouveaux"))
                {
                    textBlock.Text = $"{lowStockCount} Alertes";
                }
            }
        }
    }

    // Extension pour trouver les enfants d'un contrôle
    public static class ControlExtensions
    {
        public static System.Collections.Generic.IEnumerable<T> FindChildren<T>(this System.Windows.DependencyObject parent) where T : System.Windows.DependencyObject
        {
            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < count; i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                if (child is T childType)
                {
                    yield return childType;
                }

                foreach (var descendant in FindChildren<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}
