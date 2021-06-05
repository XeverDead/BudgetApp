using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using UI.Models;
using UI.ViewModels;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoryModificationPage.xaml
    /// </summary>
    public partial class CategoryModificationPage : Page
    {
        public CategoryModificationPage(CategoryModel categoryModel)
        {
            InitializeComponent();

            DataContext = new CategoryModificationViewModel(
                categoryModel,
                ServiceProviderContainer.GetService<CategoryService>()
                );

            ApplyButton.IsEnabled = !string.IsNullOrWhiteSpace(NameTextBox.Text);

            if (categoryModel == null)
            {
                WindowTitle = "Создание категории";
            }
        }

        private void CloseParentWindow(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyButton.IsEnabled = !string.IsNullOrWhiteSpace(NameTextBox.Text);
        }
    }
}
