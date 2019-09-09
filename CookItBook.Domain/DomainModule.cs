using CookItBook.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CookItBook.Domain
{
    public class DomainModule
    {
        private readonly IServiceCollection services;

        public DomainModule(IServiceCollection serviceCollection)
        {
            services = serviceCollection;
            Load(services);
        }

        private void Load(IServiceCollection services)
        {
            services.AddSingleton<IRecipeManager, RecipeManager>();
            services.AddSingleton<IRecipeRepository, RecipeRepository>();
        }
    }
}