using GESTION_DES_STOCKS.Models;
using System;
using System.Windows;

namespace GESTION_DES_STOCKS
{
    public partial class AddProductWindow : Window
    {
        public Product NewProduct { get; private set; } = new Product();


        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérification que tous les champs sont remplis
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(StockQuantityTextBox.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Conversion et validation du prix
            if (!double.TryParse(PriceTextBox.Text, out double price))
            {
                MessageBox.Show("Veuillez entrer un nombre valide pour le prix.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Conversion et validation de la quantité
            if (!int.TryParse(StockQuantityTextBox.Text, out int stockQuantity))
            {
                MessageBox.Show("Veuillez entrer un nombre valide pour la quantité en stock.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Crée un nouveau produit avec les données entrées
            NewProduct = new Product
            {
                ProductName = ProductNameTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Price = price,
                StockQuantity = stockQuantity
            };

            // Indique que l'ajout a réussi et ferme la fenêtre
            this.DialogResult = true;
            this.Close();
        }
    }
}
