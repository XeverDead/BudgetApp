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
    /// Логика взаимодействия для BudgetDataPage.xaml
    /// </summary>
    public partial class BudgetDataPage : Page
    {
        public BudgetDataPage()
        {
            InitializeComponent();

            DataContext = new BudgetDataViewModel(
                ServiceProviderContainer.GetService<RecordService>(), 
                ServiceProviderContainer.GetService<CategoryService>()
                );
        }
    }
}
