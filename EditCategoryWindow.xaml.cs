using GESTION_DES_STOCKS.Models;
using System.Windows;

namespace GESTION_DES_STOCKS
{
    public partial class EditCategoryWindow : Window
    {
        public Category SelectedCategory { get; private set; }

        public EditCategoryWindow(Category category)
        {
            InitializeComponent();

            // Initialise les champs avec les données actuelles de la catégorie
            SelectedCategory = category;
            CategoryNameTextBox.Text = SelectedCategory.CategoryName;
            DescriptionTextBox.Text = SelectedCategory.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Met à jour les informations de la catégorie sélectionnée
            SelectedCategory.CategoryName = CategoryNameTextBox.Text;
            SelectedCategory.Description = DescriptionTextBox.Text;

            // Valide la modification
            this.DialogResult = true;
            this.Close();
        }
    }
}
