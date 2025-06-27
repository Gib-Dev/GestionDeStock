using GESTION_DES_STOCKS.Models;  // Assurez-vous que le modèle Rapport est bien défini dans ce namespace.
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GESTION_DES_STOCKS
{
    public partial class RapportsControl : UserControl
    {
        public ObservableCollection<Rapport> Rapports { get; private set; }

        public RapportsControl()
        {
            InitializeComponent();

            // Initialisation des rapports pour l'exemple
            Rapports = new ObservableCollection<Rapport>
            {
                new Rapport { RapportID = 1, RapportName = "Rapport A", Description = "Description A", Quantity = 100 },
                new Rapport { RapportID = 2, RapportName = "Rapport B", Description = "Description B", Quantity = 50 }
            };

            // Appel pour générer les cartes de rapport
            GenerateReportCards();
        }

        // Méthode pour générer les cartes de rapport
        private void GenerateReportCards()
        {
            RapportWrapPanel.Children.Clear();  // Efface les cartes précédentes

            foreach (var rapport in Rapports)
            {
                // Création de la carte pour chaque rapport
                Border card = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                    Width = 250,
                    Background = Brushes.White
                };

                StackPanel stackPanel = new StackPanel();
                stackPanel.Children.Add(new TextBlock
                {
                    Text = $"Nom du Rapport : {rapport.RapportName}",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(106, 0, 244)),
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 5)
                });

                stackPanel.Children.Add(new TextBlock
                {
                    Text = $"Description : {rapport.Description}",
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(0, 5, 0, 5)
                });

                stackPanel.Children.Add(new TextBlock
                {
                    Text = $"Quantité : {rapport.Quantity}",
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 5, 0, 5)
                });

                stackPanel.Children.Add(new ProgressBar
                {
                    Value = rapport.Quantity,
                    Maximum = 100,
                    Height = 10,
                    Background = Brushes.LightGray,
                    Foreground = rapport.Quantity > 50 ? Brushes.Green : (rapport.Quantity > 20 ? Brushes.Orange : Brushes.Red),
                    Margin = new Thickness(0, 5, 0, 10)
                });

                // Boutons de la carte
                StackPanel buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };

                Button genererButton = new Button
                {
                    Content = "Générer",
                    Width = 100,
                    Height = 30,
                    Background = Brushes.DarkCyan,
                    Foreground = Brushes.White,
                    Margin = new Thickness(5)
                };
                genererButton.Click += (sender, e) => GenererRapport_Click(sender, e, rapport);

                Button exporterButton = new Button
                {
                    Content = "Exporter",
                    Width = 100,
                    Height = 30,
                    Background = Brushes.Orange,
                    Foreground = Brushes.White,
                    Margin = new Thickness(5)
                };
                exporterButton.Click += (sender, e) => ExporterRapport_Click(sender, e, rapport);

                buttonPanel.Children.Add(genererButton);
                buttonPanel.Children.Add(exporterButton);
                stackPanel.Children.Add(buttonPanel);

                card.Child = stackPanel;
                RapportWrapPanel.Children.Add(card);
            }
        }

        // Méthode pour générer un rapport (simulé avec un pop-up)
        private void GenererRapport_Click(object sender, RoutedEventArgs e, Rapport rapport)
        {
            MessageBox.Show($"ID: {rapport.RapportID}\nNom : {rapport.RapportName}\nDescription : {rapport.Description}\nQuantité : {rapport.Quantity}",
                            "Rapport Généré", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Méthode pour exporter un rapport en CSV
        private void ExporterRapport_Click(object sender, RoutedEventArgs e, Rapport rapport)
        {
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("ID,RapportName,Description,Quantité");
            csvContent.AppendLine($"{rapport.RapportID},{rapport.RapportName},{rapport.Description},{rapport.Quantity}");

            string filePath = $"Rapport_{rapport.RapportName}.csv";
            File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);

            MessageBox.Show($"Rapport exporté avec succès dans le fichier : {filePath}", "Exportation", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
