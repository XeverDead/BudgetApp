using System;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public static class ServiceProviderContainer
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
