using System.Windows;
using System.Windows.Controls;

namespace GESTION_DES_STOCKS
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation avant d'accéder aux éléments
            if (EnableNotificationsCheckBox == null || NotificationThresholdTextBox == null ||
                ThemeComboBox == null || ExportFormatComboBox == null)
            {
                MessageBox.Show("Erreur : certains éléments de configuration ne sont pas disponibles.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool enableNotifications = EnableNotificationsCheckBox.IsChecked ?? false;
            string threshold = NotificationThresholdTextBox.Text;
            string theme = ((ComboBoxItem)ThemeComboBox.SelectedItem)?.Content?.ToString() ?? "Clair";
            string exportFormat = ((ComboBoxItem)ExportFormatComboBox.SelectedItem)?.Content?.ToString() ?? "CSV";

            MessageBox.Show($"Paramètres sauvegardés:\n\n" +
                            $"Notifications: {(enableNotifications ? "Activé" : "Désactivé")}\n" +
                            $"Seuil: {threshold}\n" +
                            $"Mode: {theme}\n" +
                            $"Format d'exportation: {exportFormat}",
                            "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ResetSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            EnableNotificationsCheckBox.IsChecked = false;
            NotificationThresholdTextBox.Text = "10";
            ThemeComboBox.SelectedIndex = 0;
            ExportFormatComboBox.SelectedIndex = 0;

            MessageBox.Show("Les paramètres ont été réinitialisés aux valeurs par défaut.", "Réinitialisation", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
