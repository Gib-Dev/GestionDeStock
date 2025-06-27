using GESTION_DES_STOCKS.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GESTION_DES_STOCKS
{
    public partial class CategoriesControl : UserControl
    {
        public ObservableCollection<Category> Categories { get; private set; }

        public CategoriesControl()
        {
            InitializeComponent();

            // Initialisation des catégories pour exemple
            Categories = new ObservableCollection<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Catégorie A", Description = "Description A" },
                new Category { CategoryID = 2, CategoryName = "Catégorie B", Description = "Description B" }
            };

            CategoryDataGrid.ItemsSource = Categories;
        }

        private void AjouterCategorie_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour ajouter une nouvelle catégorie
            Category newCategory = new Category { CategoryID = Categories.Count + 1, CategoryName = "Nouvelle Catégorie", Description = "Description..." };
            Categories.Add(newCategory);
        }

        private void ModifierCategorie_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                // Ouvre la fenêtre de modification de catégorie en passant la catégorie sélectionnée
                EditCategoryWindow editWindow = new EditCategoryWindow(selectedCategory);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                {
                    // Rafraîchit l'affichage de la grille après la modification
                    CategoryDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void SupprimerCategorie_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                Categories.Remove(selectedCategory);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RechercherCategorie_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchCategoryBox.Text.ToLower();
            var filteredCategories = new ObservableCollection<Category>();

            foreach (var category in Categories)
            {
                if (category.CategoryName.ToLower().Contains(searchTerm) || category.Description.ToLower().Contains(searchTerm))
                {
                    filteredCategories.Add(category);
                }
            }

            CategoryDataGrid.ItemsSource = filteredCategories;

            if (filteredCategories.Count == 0)
            {
                MessageBox.Show("Aucune catégorie trouvée.", "Résultat de recherche", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Rechercher une catégorie...")
            {
                tb.Text = "";
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Rechercher une catégorie...";
                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
