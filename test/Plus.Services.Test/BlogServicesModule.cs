using Plus.AutoMapper;
using Plus.Modules;
using Plus.Services.Dto.Test;
using System.Reflection;

namespace Plus.Services.Test
{
    [DependsOn(
       typeof(PluseAutoMapperModule),
       typeof(BlogServicesDtoModule)
   )]
    public class BlogServicesModule : PlusModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}