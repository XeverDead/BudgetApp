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
using UI.ViewModels;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordsListPage.xaml
    /// </summary>
    public partial class RecordsListPage : Page
    {
        private readonly RecordsListViewModel _viewModel;

        public RecordsListPage()
        {
            _viewModel = new RecordsListViewModel(
                ServiceProviderContainer.GetService<RecordService>(),
                ServiceProviderContainer.GetService<CategoryService>()
                );
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var recordPage = new RecordModificationPage(null);

            var dialogWindow = new DialogWindow()
            {
                Owner = Window.GetWindow(this),
                Content = recordPage
            };

            dialogWindow.ShowDialog();

            _viewModel.SetRecordModels();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentRecordModelNotNull)
            {
                var recordPage = new RecordModificationPage(_viewModel.CurrentRecordModel);

                var dialogWindow = new DialogWindow()
                {
                    Owner = Window.GetWindow(this),
                    Content = recordPage
                };

                dialogWindow.ShowDialog();

                _viewModel.SetRecordModels();
            }
        }
    }
}
