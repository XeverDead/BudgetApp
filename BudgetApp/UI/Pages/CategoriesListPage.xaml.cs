using BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            SetEventHandlers();
        }

        private void SetEventHandlers()
        {
            ChangeButton.Click += ChangeButton_Click;
            AddButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryPage = new CategoryModificationPage(null);
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentCategoryModelNotNull)
            {
                var categoryPage = new CategoryModificationPage(_viewModel.CurrentCategoryModel);
            } 
        }
    }
}
