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
        private CategoriesListViewModel _viewModel;

        public CategoriesListPage()
        {
            InitializeComponent();

            _viewModel = new CategoriesListViewModel(ServiceProviderContainer.GetService<CategoryService>());
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryPage = new CategoryModificationPage(null);

            var dialogWindow = new DialogWindow()
            {
                Owner = Window.GetWindow(this),
                Content = categoryPage
            };

            dialogWindow.ShowDialog();

            _viewModel = new CategoriesListViewModel(ServiceProviderContainer.GetService<CategoryService>());
            DataContext = _viewModel;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentCategoryModelNotNull)
            {
                var categoryPage = new CategoryModificationPage(_viewModel.CurrentCategoryModel);

                var dialogWindow = new DialogWindow()
                {
                    Owner = Window.GetWindow(this),
                    Content = categoryPage
                };

                dialogWindow.ShowDialog();

                _viewModel = new CategoriesListViewModel(ServiceProviderContainer.GetService<CategoryService>());
                DataContext = _viewModel;
            } 
        }
    }
}
