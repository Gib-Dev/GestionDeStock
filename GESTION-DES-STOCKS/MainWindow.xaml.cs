using System.Windows;

namespace GESTION_DES_STOCKS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Gestion de l'événement du bouton "Login"
        private void Connecter_Click(object sender, RoutedEventArgs e)
        {
            string username = NomUser.Text;
            string password = MotDePasse.Password;

            // Vérification simple des identifiants (exemple de validation)
            if (username == "bonjour" && password == "bonjour")
            {
                // Afficher un message de succès
                MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Naviguer vers la page d'accueil
                Homepage homeWindow = new Homepage();
                homeWindow.Show();

                // Fermer la fenêtre de connexion
                this.Close();
            }
            else
            {
                ErreurMessage.Content = "Identifiants incorrects. Veuillez réessayer.";
                ErreurMessage.Opacity = 1; // Afficher le message d'erreur
            }
        }

        // Gestion de l'événement pour le lien "Créer un compte"
        private void CreerCompte_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Redirection vers la page de création de compte.", "Créer un compte", MessageBoxButton.OK, MessageBoxImage.Information);
            // Logique pour ouvrir la fenêtre de création de compte ou naviguer vers cette page
        }

        // Gestion de l'événement pour le lien "Mot de passe oublié"
        private void ReinitialiserMDP_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Redirection vers la page de réinitialisation de mot de passe.", "Mot de passe oublié", MessageBoxButton.OK, MessageBoxImage.Information);
            // Logique pour ouvrir la fenêtre de réinitialisation de mot de passe ou envoyer un email de réinitialisation
        }

        // Gestion de l'événement pour le bouton "X" pour fermer l'application
        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
