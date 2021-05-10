using System.Windows;
using System.Windows.Controls;
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
            ShowRecordModificationWindow(new RecordModificationPage(null));

            UpdateViewModelServices();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCurrentRecordModelNotNull)
            {
                ShowRecordModificationWindow(new RecordModificationPage(_viewModel.CurrentRecordModel));

                UpdateViewModelServices();
            }
        }

        private void ShowRecordModificationWindow(RecordModificationPage content)
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
            _viewModel.UpdateServices(
                ServiceProviderContainer.GetService<RecordService>(),
                ServiceProviderContainer.GetService<CategoryService>()
                );
        }
    }
}
