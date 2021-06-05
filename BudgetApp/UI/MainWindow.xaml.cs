using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Pages;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CategoriesListTabHeaderLabel.MouseLeftButtonDown += CategoriesListTabHeaderLabel_MouseLeftButtonDown;
            RecordsListTabHeaderLabel.MouseLeftButtonDown += RecordsListTabHeaderLabel_MouseLeftButtonDown;
        }

        private void BudgetDataTabHeaderLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var budgetDataPage = BudgetDataFrame.Content as BudgetDataPage;
            budgetDataPage.UpdateViewModelServices();

            RemoveEventHandlers();
            CategoriesListTabHeaderLabel.MouseLeftButtonDown += CategoriesListTabHeaderLabel_MouseLeftButtonDown;
            RecordsListTabHeaderLabel.MouseLeftButtonDown += RecordsListTabHeaderLabel_MouseLeftButtonDown;
        }

        private void CategoriesListTabHeaderLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveEventHandlers();
            RecordsListTabHeaderLabel.MouseLeftButtonDown += RecordsListTabHeaderLabel_MouseLeftButtonDown;
            BudgetDataTabHeaderLabel.MouseLeftButtonDown += BudgetDataTabHeaderLabel_MouseLeftButtonDown;
        }

        private void RecordsListTabHeaderLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var recordsListPage = RecordsListFrame.Content as RecordsListPage;
            recordsListPage.UpdateViewModelServices();

            RemoveEventHandlers();
            CategoriesListTabHeaderLabel.MouseLeftButtonDown += CategoriesListTabHeaderLabel_MouseLeftButtonDown;
            BudgetDataTabHeaderLabel.MouseLeftButtonDown += BudgetDataTabHeaderLabel_MouseLeftButtonDown;
        }

        private void RemoveEventHandlers()
        {
            CategoriesListTabHeaderLabel.MouseLeftButtonDown -= CategoriesListTabHeaderLabel_MouseLeftButtonDown;
            RecordsListTabHeaderLabel.MouseLeftButtonDown -= RecordsListTabHeaderLabel_MouseLeftButtonDown;
            BudgetDataTabHeaderLabel.MouseLeftButtonDown -= BudgetDataTabHeaderLabel_MouseLeftButtonDown;
        }
    }
}
