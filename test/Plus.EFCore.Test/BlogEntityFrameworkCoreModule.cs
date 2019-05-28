using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using Plus.Core.Tests;
using Plus.Core.Tests.Configuration;
using Plus.EntityFramework;
using Plus.Modules;

namespace Plus.EFCore.Test
{
    [DependsOn(
        typeof(BlogCoreModule),
        typeof(PlusEntityFrameworkModule)
    )]
    public class BlogEntityFrameworkCoreModule : PlusModule
    {
        public override void PreInitialize()
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseMySql(AppSettings.MySqlConnectionString);

            IocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<BlogDbContext>>()
                    .Instance(builder.Options)
                    .LifestyleSingleton()
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssembly(typeof(BlogEntityFrameworkCoreModule).GetAssembly());
        }
    }
}