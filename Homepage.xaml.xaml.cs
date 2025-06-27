using System.Windows;
using GESTION_DES_STOCKS.Models;
using GESTION_DES_STOCKS.Services;
using GESTION_DES_STOCKS.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GESTION_DES_STOCKS.Views
{
    public partial class Homepage : Window
    {
        private readonly User _currentUser;
        private readonly IServiceProvider _serviceProvider;

        public Homepage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            
            // Configurer les services
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            
            // Afficher les informations de l'utilisateur
            UpdateUserInfo();
            
            // Charger le tableau de bord par défaut
            MainContent.Content = new DashboardControl(_serviceProvider);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StockDbContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
        }

        private void UpdateUserInfo()
        {
            // Mettre à jour le titre avec le nom de l'utilisateur
            var titleText = $"Gestion des Stocks - Bienvenue {_currentUser.FirstName} {_currentUser.LastName}";
            this.Title = titleText;
            
            // Vous pouvez aussi ajouter un indicateur de rôle dans l'interface
            if (_currentUser.Role == UserRole.Admin)
            {
                // Afficher des options supplémentaires pour les administrateurs
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Tableau de Bord";
            MainContent.Content = new DashboardControl(_serviceProvider);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Gestion des Produits";
            MainContent.Content = new ProductsControl(_serviceProvider);
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Catégories";
            MainContent.Content = new CategoriesControl(_serviceProvider);
        }

        private void Stocks_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Stocks";
            MainContent.Content = new StocksControl(_serviceProvider);
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Rapports";
            MainContent.Content = new RapportsControl(_serviceProvider);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Paramètres";
            MainContent.Content = new SettingsControl(_serviceProvider, _currentUser);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?", 
                "Déconnexion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
            if (result == MessageBoxResult.Yes)
            {
                // Retourner à la fenêtre de connexion
                var loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
