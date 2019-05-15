using Castle.MicroKernel.Registration;
using Plus.Collections;
using Plus.Configuration.Startup;
using Plus.Dependency;
using Plus.Event.Bus;
using Plus.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Plus
{
    public class PlusLeadershipModule : PlusModule
    {
        public override void PreInitialize()
        {
            IocManager.AddRegistrar(new BasicConventionalRegistrar());
            ConfigureCaches();
            AddIgnoredTypes();
            AddMethodParameterValidators();
        }

        public override void Initialize()
        {
            foreach (Action value in ((PlusStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                value();
            }

            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));


            IocManager.RegisterAssembly(typeof(PlusLeadershipModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();
        }

        public override void Shutdown()
        {
        }

        private void RegisterMissingComponents()
        {
            if (!IocManager.IsRegistered<IGuidGenerator>())
            {
                IocManager.IocContainer.Register(
                    Component
                        .For<IGuidGenerator, SequentialGuidGenerator>()
                        .Instance(SequentialGuidGenerator.Instance)
                );
            }

            //IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            //IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>();
        }

        private void AddMethodParameterValidators()
        {
            //Configuration.Validation.Validators.Add<DataAnnotationsValidator>();
            //Configuration.Validation.Validators.Add<ValidatableObjectValidator>();
        }

        private void AddIgnoredTypes()
        {
            var commonIgnoredTypes = new[]
            {
                typeof(Stream),
                typeof(Expression)
            };

            foreach (var ignoredType in commonIgnoredTypes)
            {
                //Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                //Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }

            var validationIgnoredTypes = new[] { typeof(Type) };
            foreach (var ignoredType in validationIgnoredTypes)
            {
                //Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }

        private void ConfigureCaches()
        {
        }

    }
}