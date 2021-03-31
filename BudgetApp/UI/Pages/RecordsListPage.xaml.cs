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

            SetEventHandlers();
        }

        private void SetEventHandlers()
        {
            ChangeButton.Click += ChangeButton_Click;
            AddButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var recordPage = new RecordModificationPage(null);
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentRecordModelNotNull)
            {
                var recordPage = new RecordModificationPage(_viewModel.CurrentRecordModel);
            }
        }
    }
}
