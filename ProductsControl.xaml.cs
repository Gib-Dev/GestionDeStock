using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GESTION_DES_STOCKS.Models;
using GESTION_DES_STOCKS.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GESTION_DES_STOCKS
{
    public partial class ProductsControl : UserControl
    {
        private readonly IProductService _productService;
        private ObservableCollection<Product> _products;

        public ProductsControl(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productService = serviceProvider.GetRequiredService<IProductService>();
            _products = new ObservableCollection<Product>();
            
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                _products.Clear();
                foreach (var product in products)
                {
                    _products.Add(product);
                }
                ProductDataGrid.ItemsSource = _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des produits: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addProductWindow = new AddProductWindow();
                bool? result = addProductWindow.ShowDialog();

                if (result == true && addProductWindow.NewProduct != null)
                {
                    var success = await _productService.AddProductAsync(addProductWindow.NewProduct);
                    if (success)
                    {
                        MessageBox.Show("Produit ajouté avec succès !", "Succès", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadProductsAsync(); // Recharger la liste
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout du produit. Vérifiez les données saisies.", 
                            "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du produit: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductDataGrid.SelectedItem is Product selectedProduct)
                {
                    EditProductWindow editWindow = new EditProductWindow(selectedProduct);
                    bool? result = editWindow.ShowDialog();

                    if (result == true)
                    {
                        var success = await _productService.UpdateProductAsync(selectedProduct);
                        if (success)
                        {
                            MessageBox.Show("Produit modifié avec succès !", "Succès", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadProductsAsync(); // Recharger la liste
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la modification du produit. Vérifiez les données saisies.", 
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un produit à modifier.", 
                        "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification du produit: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductDataGrid.SelectedItem is Product selectedProduct)
                {
                    var result = MessageBox.Show(
                        $"Êtes-vous sûr de vouloir supprimer le produit '{selectedProduct.ProductName}' ?", 
                        "Confirmation de suppression", 
                        MessageBoxButton.YesNo, 
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var success = await _productService.DeleteProductAsync(selectedProduct.ProductID);
                        if (success)
                        {
                            MessageBox.Show("Produit supprimé avec succès !", "Succès", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadProductsAsync(); // Recharger la liste
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression du produit.", 
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un produit à supprimer.", 
                        "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du produit: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RechercherProduit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchTerm = SearchProductBox.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadProductsAsync(); // Afficher tous les produits
                }
                else
                {
                    var filteredProducts = await _productService.SearchProductsAsync(searchTerm);
                    _products.Clear();
                    foreach (var product in filteredProducts)
                    {
                        _products.Add(product);
                    }

                    if (filteredProducts.Count == 0)
                    {
                        MessageBox.Show("Aucun produit trouvé correspondant à votre recherche.", 
                            "Recherche", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchProductBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                RechercherProduit_Click(sender, e);
            }
        }
    }
}
