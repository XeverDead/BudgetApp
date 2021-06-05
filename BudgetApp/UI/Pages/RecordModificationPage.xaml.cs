using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL.Services;
using UI.Models;
using UI.ViewModels;
using System.Globalization;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordModificationPage.xaml
    /// </summary>
    public partial class RecordModificationPage : Page
    {
        private readonly RecordModificationViewModel _viewModel;

        public RecordModificationPage(RecordModel recordModel)
        {
            InitializeComponent();

            _viewModel = new RecordModificationViewModel(
                recordModel,
                ServiceProviderContainer.GetService<RecordService>(),
                ServiceProviderContainer.GetService<CategoryService>()
                );

            DataContext = _viewModel;

            if (recordModel == null)
            {
                WindowTitle = "Создание записи";
            }
        }

        private void CloseParentWindow(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var categorySelectionPage = new CategorySelectionPage();

            var dialogWindow = new DialogWindow()
            {
                Content = categorySelectionPage
            };

            if (dialogWindow.ShowDialog() == true)
            {
                _viewModel.AddCategoryRecord(categorySelectionPage.SelectedCategoryModel);
            }
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = e.Source as TextBox;

            var fullNumber = (textBox.Text + e.Text + "0").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            if (!(int.TryParse(e.Text, out int _) || double.TryParse(fullNumber, NumberStyles.Any, CultureInfo.CurrentCulture, out double _))) 
            {
                e.Handled = true;
            }
        }

        private void DeleteButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dialogResult = MessageBox.Show("Вы точно хотите удалить эту запись?", "Удаление записи", MessageBoxButton.YesNo);

            e.Handled = dialogResult == MessageBoxResult.No;

            if (!e.Handled)
            {
                _viewModel.DeleteCommand.Execute(null);
                Window.GetWindow(this).Close();
            }
        }
    }
}
