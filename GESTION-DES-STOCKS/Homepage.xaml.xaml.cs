using System.Windows;
using System.Windows.Controls;

namespace GESTION_DES_STOCKS
{
    public partial class Homepage : Window
    {
        public Homepage()
        {
            InitializeComponent();
        }

        // Gestionnaire de clic pour le bouton "Tableau de Bord"
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Tableau de Bord";
            // Logique pour afficher le tableau de bord
        }

        // Gestionnaire de clic pour le bouton "Gestion des Produits"
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Gestion des Produits";
            // Logique pour afficher la gestion des produits
        }

        // Gestionnaire de clic pour le bouton "Catégories"
        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Catégories";
            // Logique pour afficher les catégories
        }

        // Gestionnaire de clic pour le bouton "Stocks"
        private void Stocks_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Stocks";
            // Logique pour afficher les informations sur les stocks
        }

        // Gestionnaire de clic pour le bouton "Rapports"
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Rapports";
            // Logique pour afficher les rapports
        }

        // Gestionnaire de clic pour le bouton "Paramètres"
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Paramètres";
            // Logique pour afficher les paramètres
        }

        // Gestionnaire de clic pour le bouton "Déconnexion"
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour la déconnexion, par exemple fermeture de la fenêtre ou retour à l'écran de connexion
            MessageBox.Show("Déconnexion réussie.");
            this.Close(); // Ferme la fenêtre actuelle
        }
    }
}
