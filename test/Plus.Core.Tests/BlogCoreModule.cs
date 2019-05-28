using Plus.Modules;
using System.Reflection;

namespace Plus.Core.Tests
{
    [DependsOn(typeof(PlusLeadershipModule))]
    public class BlogCoreModule : PlusModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}