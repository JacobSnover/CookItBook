using System;
using System.Collections.Generic;
using System.Text;

namespace CookItBook.Infrastructure.Orchestrators
{
    public interface IRecipeOrchestrator
    {
        string Create();

        Recipe Retreive();
    }
}
