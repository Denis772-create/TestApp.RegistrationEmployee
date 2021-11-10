using System;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Collections.Generic;
using System.Reflection;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Core.Interfaces;
using TestApp.RegistrationEmployee.Infrastructure.Data;
using TestApp.RegistrationEmployee.Infrastructure.Services;
using Module = Autofac.Module;

namespace TestApp.RegistrationEmployee.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(Assembly callingAssembly = null)
        {
            var coreAssembly = Assembly.GetAssembly(typeof(Company));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
            _assemblies.Add(coreAssembly);
            _assemblies.Add(infrastructureAssembly);

            if (callingAssembly != null)
                _assemblies.Add(callingAssembly);
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<IEmailSender>()
                .As(typeof(EmailSender))
                .InstancePerLifetimeScope();
        }

    }
}
