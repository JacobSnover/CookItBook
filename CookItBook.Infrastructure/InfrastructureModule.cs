using System;
using CookItBook.Infrastructure.Orchestrators;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookItBook.Infrastructure
{
    public class InfrastructureModule
    {

        public static void Load(IServiceCollection services)
        {
            services.AddSingleton<IRecipeOrchestrator, RecipeOrchestrator>();
        }

        
    }
}
