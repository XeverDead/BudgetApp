using BLL.Services;
using System.Windows;
using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoriesListPage.xaml
    /// </summary>
    public partial class CategoriesListPage : Page
    {
        private readonly CategoriesListViewModel _viewModel;

        public CategoriesListPage()
        {
            InitializeComponent();

            _viewModel = new CategoriesListViewModel(ServiceProviderContainer.GetService<CategoryService>());
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ShowCategoryModificationWindow(new CategoryModificationPage(null));

            UpdateViewModelServices();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentCategoryModelNotNull)
            {
                ShowCategoryModificationWindow(new CategoryModificationPage(_viewModel.CurrentCategoryModel));

                UpdateViewModelServices();
            } 
        }

        private void ShowCategoryModificationWindow(CategoryModificationPage content)
        {
            var dialogWindow = new DialogWindow()
            {
                Owner = Window.GetWindow(this),
                Content = content
            };

            dialogWindow.ShowDialog();
        }

        public void UpdateViewModelServices()
        {
            _viewModel.UpdateServices(ServiceProviderContainer.GetService<CategoryService>());
        }
    }
}
