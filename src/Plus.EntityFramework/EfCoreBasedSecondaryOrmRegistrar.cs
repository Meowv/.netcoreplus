using System;

namespace Plus.EntityFramework
{
    /// <summary>
    /// EfCoreBasedSecondaryOrmRegistrar
    /// </summary>
    public class EfCoreBasedSecondaryOrmRegistrar : SecondaryOrmRegistrarBase
    {
        public override string OrmContextKey { get; } = "EntityFrameworkCore";


        public EfCoreBasedSecondaryOrmRegistrar(Type dbContextType, IDbContextEntityFinder dbContextEntityFinder)
            : base(dbContextType, dbContextEntityFinder)
        {
        }
    }
}