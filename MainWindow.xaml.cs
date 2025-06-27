using GESTION_DES_STOCKS.Views;
using GESTION_DES_STOCKS.Services;
using GESTION_DES_STOCKS.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GESTION_DES_STOCKS
{
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;

        public MainWindow()
        {
            InitializeComponent();
            
            // Initialiser les services
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            
            _authService = serviceProvider.GetRequiredService<IAuthService>();
            
            // Initialiser la base de données
            InitializeDatabaseAsync();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StockDbContext>();
            services.AddScoped<IAuthService, AuthService>();
        }

        private async void InitializeDatabaseAsync()
        {
            try
            {
                using var scope = new ServiceCollection()
                    .AddDbContext<StockDbContext>()
                    .BuildServiceProvider()
                    .CreateScope();
                
                var context = scope.ServiceProvider.GetRequiredService<StockDbContext>();
                await context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'initialisation de la base de données: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Connecter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = NomUser.Text.Trim();
                string password = MotDePasse.Password;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ShowError("Veuillez saisir votre nom d'utilisateur et mot de passe.");
                    return;
                }

                // Afficher un indicateur de chargement
                var button = sender as Button;
                var originalContent = button?.Content;
                button!.Content = "Connexion...";
                button.IsEnabled = false;

                var user = await _authService.AuthenticateAsync(username, password);

                if (user != null)
                {
                    // Générer un token (optionnel pour cette implémentation)
                    var token = _authService.GenerateJwtToken(user);
                    
                    // Ouvrir la page d'accueil avec les informations de l'utilisateur
                    var homeWindow = new Homepage(user);
                    homeWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowError("Nom d'utilisateur ou mot de passe incorrect.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Erreur de connexion: {ex.Message}");
            }
            finally
            {
                // Restaurer le bouton
                var button = sender as Button;
                button!.Content = "Se connecter";
                button.IsEnabled = true;
            }
        }

        private void ShowError(string message)
        {
            ErreurMessage.Content = message;
            ErreurMessage.Opacity = 1;
        }

        private void CreerCompte_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonctionnalité de création de compte à implémenter.", 
                "Créer un compte", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void ReinitialiserMDP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = Microsoft.VisualBasic.Interaction.InputBox(
                    "Veuillez saisir votre adresse email:", 
                    "Réinitialisation du mot de passe", 
                    "");

                if (!string.IsNullOrEmpty(email))
                {
                    var success = await _authService.ResetPasswordAsync(email);
                    if (success)
                    {
                        MessageBox.Show("Un nouveau mot de passe temporaire a été généré. " +
                            "Vérifiez la console de débogage pour le voir.", 
                            "Mot de passe réinitialisé", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Aucun compte trouvé avec cette adresse email.", 
                            "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la réinitialisation: {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
