using Microsoft.Extensions.DependencyInjection;
using DanceInfo.ContentSearch.Repositories;
using Sitecore.DependencyInjection;

namespace DanceInfo.Infrastructure
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllersInCurrentAssembly();
            serviceCollection.AddSingleton<ICatalogRepository, CatalogRepository>();
        }
    }
}