using BLL.Services;
using System.Windows;
using System.Windows.Controls;
using UI.Models;
using UI.ViewModels;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategorySelectionPage.xaml
    /// </summary>
    public partial class CategorySelectionPage : Page
    {
        private readonly CategoriesListViewModel _viewModel;

        public CategoryModel SelectedCategoryModel { get; private set; }

        public CategorySelectionPage()
        {
            InitializeComponent();

            _viewModel = new CategoriesListViewModel(ServiceProviderContainer.GetService<CategoryService>());
            DataContext = _viewModel;
        }

        private void CloseParentWindow()
        {
            Window.GetWindow(this).Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedCategoryModel = _viewModel.CurrentCategoryModel;

            Window.GetWindow(this).DialogResult = true;
            CloseParentWindow();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            CloseParentWindow();
        }
    }
}
