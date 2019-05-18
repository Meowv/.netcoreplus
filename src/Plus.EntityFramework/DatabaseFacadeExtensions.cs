using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Plus.EntityFramework
{
    /// <summary>
    /// DatabaseFacadeExtensions
    /// </summary>
    public static class DatabaseFacadeExtensions
    {
        public static bool IsRelational(this DatabaseFacade database)
        {
            return ServiceProviderServiceExtensions.GetService<IRelationalConnection>(AccessorExtensions.GetInfrastructure(database)) != null;
        }
    }
}