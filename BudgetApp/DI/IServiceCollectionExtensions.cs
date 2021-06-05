using BLL.Services;
using DAL;
using DAL.EFRepositories;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DI
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBudgetAppDependencies(this IServiceCollection services, string connectionString)
        {
            AddDalDependencies(services, connectionString);
            AddBllDependencies(services);
        }

        private static void AddDalDependencies(IServiceCollection services, string connectionString)
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseSqlite(connectionString)
                .Options;

            services.AddTransient((services) => new BudgetContext(options));

            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddTransient<IRecordRepository, EFRecordRepository>();
        }

        private static void AddBllDependencies(IServiceCollection services)
        {
            services.AddTransient<CategoryService>();
            services.AddTransient<RecordService>();
        }
    }
}
