using BLL.Services;
using DAL.EFRepositories;
using DAL.Interfaces;
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
            services.AddSingleton<ICategoryRepository, EFCategoryRepository>();
            services.AddSingleton<IRecordRepository, EFRecordRepository>();
        }

        private static void AddBllDependencies(IServiceCollection services)
        {
            services.AddSingleton<CategoryService>();
            services.AddSingleton<RecordService>();
        }
    }
}
