using Plus.AutoMapper;
using Plus.Modules;
using System.Reflection;

namespace Plus.Services.Dto.Test
{
    [DependsOn(typeof(PluseAutoMapperModule))]
    public class BlogServicesDtoModule : PlusModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}