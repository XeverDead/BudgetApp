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
            DataContext = new CategoryModificationViewModel(
                categoryModel,
                ServiceProviderContainer.GetService<CategoryService>()
                );

            SetEventHandlers();
        }

        private void SetEventHandlers()
        {
            ApplyButton.Click += CloseParentWindow;
            CancelButton.Click += CloseParentWindow;
        }

        private void CloseParentWindow(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
