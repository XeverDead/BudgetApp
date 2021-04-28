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
        public static void AddBudgetAppDependencies(this IServiceCollection services)
        {
            AddDalDependencies(services);
            AddBllDependencies(services);
        }

        private static void AddDalDependencies(IServiceCollection services)
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseSqlite("Filename=database.db;")
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
