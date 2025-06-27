using System.Windows;

namespace GESTION_DES_STOCKS
{
    public partial class ProductsControl : Window
    {
        public ProductsControl()
        {
            InitializeComponent();
        }

        private void AjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour ajouter un produit
            MessageBox.Show("Fonctionnalité d'ajout de produit", "Ajouter Produit");
        }

        private void ModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour modifier un produit
            MessageBox.Show("Fonctionnalité de modification de produit", "Modifier Produit");
        }

        private void SupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour supprimer un produit
            MessageBox.Show("Fonctionnalité de suppression de produit", "Supprimer Produit");
        }

        private void RechercherProduit_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour rechercher un produit
            MessageBox.Show("Fonctionnalité de recherche de produit", "Rechercher Produit");
        }
    }
}
