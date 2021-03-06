using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DI;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddBudgetAppDependencies("Filename=BudgetApp.db;");

            ServiceProviderContainer.SetServiceProvider(services.BuildServiceProvider());
        }
    }
}
