using System;
using System.Windows;
using GESTION_DES_STOCKS.Models;


namespace GESTION_DES_STOCKS
{
    public partial class EditProductWindow : Window
    {
        public Product SelectedProduct { get; set; }

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            SelectedProduct = product;
            ProductNameTextBox.Text = SelectedProduct.ProductName;
            DescriptionTextBox.Text = SelectedProduct.Description;
            PriceTextBox.Text = SelectedProduct.Price.ToString();
            StockQuantityTextBox.Text = SelectedProduct.StockQuantity.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que tous les champs requis sont remplis
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(StockQuantityTextBox.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Affectation des valeurs des champs texte aux propriétés du produit
            SelectedProduct.ProductName = ProductNameTextBox.Text;
            SelectedProduct.Description = DescriptionTextBox.Text;

            // Conversion explicite en double ou decimal pour `Price`
            if (double.TryParse(PriceTextBox.Text, out double price))
            {
                SelectedProduct.Price = price;
            }
            else if (decimal.TryParse(PriceTextBox.Text, out decimal priceDecimal))
            {
                SelectedProduct.Price = (double)priceDecimal;
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nombre valide pour le prix.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Conversion et validation de la quantité en stock
            if (int.TryParse(StockQuantityTextBox.Text, out int stockQuantity))
            {
                SelectedProduct.StockQuantity = stockQuantity;
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nombre valide pour la quantité en stock.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Indique que les modifications ont été enregistrées avec succès
            this.DialogResult = true;
            this.Close();
        }

    }
}
