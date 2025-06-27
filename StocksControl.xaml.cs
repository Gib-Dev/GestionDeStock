using GESTION_DES_STOCKS.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GESTION_DES_STOCKS
{
    public partial class StocksControl : UserControl
    {
        public ObservableCollection<Stock> Stocks { get; private set; }

        public StocksControl()
        {
            InitializeComponent();

            // Exemple d'initialisation des stocks
            Stocks = new ObservableCollection<Stock>
            {
                new Stock { StockID = 1, StockName = "Stock A", Description = "Description A", Quantity = 100 },
                new Stock { StockID = 2, StockName = "Stock B", Description = "Description B", Quantity = 50 }
            };

            StockDataGrid.ItemsSource = Stocks;
        }

        // Fonctionnalité du bouton "Ajouter"
        private void AjouterStock_Click(object sender, RoutedEventArgs e)
        {
            // Crée un nouveau stock
            var newStock = new Stock { StockID = Stocks.Count + 1, StockName = "Nouveau Stock", Description = "Description...", Quantity = 0 };
            Stocks.Add(newStock);
            MessageBox.Show("Un nouveau stock a été ajouté.", "Ajouter Stock", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Fonctionnalité du bouton "Modifier"
        private void ModifierStock_Click(object sender, RoutedEventArgs e)
        {
            if (StockDataGrid.SelectedItem is Stock selectedStock)
            {
                // Exemple de modification
                selectedStock.StockName = "Stock Modifié";
                selectedStock.Description = "Description modifiée";
                StockDataGrid.Items.Refresh(); // Actualise l'affichage
                MessageBox.Show($"Le stock '{selectedStock.StockName}' a été modifié.", "Modifier Stock", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un stock à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Fonctionnalité du bouton "Supprimer"
        private void SupprimerStock_Click(object sender, RoutedEventArgs e)
        {
            if (StockDataGrid.SelectedItem is Stock selectedStock)
            {
                Stocks.Remove(selectedStock);
                MessageBox.Show("Le stock a été supprimé.", "Supprimer Stock", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un stock à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Fonctionnalité de recherche
        private void RechercherStock_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchStockBox.Text.ToLower();
            var filteredStocks = new ObservableCollection<Stock>();

            foreach (var stock in Stocks)
            {
                if (stock.StockName.ToLower().Contains(searchTerm) || stock.Description.ToLower().Contains(searchTerm))
                {
                    filteredStocks.Add(stock);
                }
            }

            StockDataGrid.ItemsSource = filteredStocks;

            if (filteredStocks.Count == 0)
            {
                MessageBox.Show("Aucun stock trouvé.", "Résultat de recherche", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Placeholder pour la barre de recherche
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Rechercher un stock...")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Rechercher un stock...";
                tb.Foreground = Brushes.Gray;
            }
        }
    }
}
