using BLL.Services;
using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для BudgetDataPage.xaml
    /// </summary>
    public partial class BudgetDataPage : Page
    {
        private readonly BudgetDataViewModel _viewModel;

        public BudgetDataPage()
        {
            InitializeComponent();

            _viewModel = new BudgetDataViewModel(
                ServiceProviderContainer.GetService<RecordService>(),
                ServiceProviderContainer.GetService<CategoryService>()
                );

            DataContext = _viewModel;
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
