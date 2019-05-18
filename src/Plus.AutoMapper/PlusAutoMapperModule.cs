using AutoMapper;
using Castle.MicroKernel.Registration;
using Plus.Configuration.Startup;
using Plus.Modules;
using Plus.Reflection;
using System;
using System.Reflection;
using IObjectMapper = Plus.ObjectMapping.IObjectMapper;

namespace Plus.AutoMapper
{
    /// <summary>
    /// PluseAutoMapperModule
    /// </summary>
    [DependsOn(typeof(PlusLeadershipModule))]
    public class PluseAutoMapperModule : PlusModule
    {
        private readonly ITypeFinder _typeFinder;

        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public PluseAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IPlusAutoMapperConfiguration, PlusAutoMapperConfiguration>();

            Configuration.ReplaceService<IObjectMapper, AutoMapperObjectMapper>();
        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.PlusAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                };
                if (Configuration.PlusAutoMapper().UseStaticMapper)
                {
                    // 防止重复映射
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }

                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(Mapper.Configuration).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
                    );
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(config).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return type.IsDefined(typeof(AutoMapAttribute)) ||
                       type.IsDefined(typeof(AutoMapFromAttribute)) ||
                       type.IsDefined(typeof(AutoMapToAttribute));
            });

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }
    }
}