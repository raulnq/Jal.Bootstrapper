using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.CastleWindsor
{
    public class WindsorBootstrapper : IBootstrapper<IWindsorContainer>
    {
        private readonly IEnumerable<IWindsorInstaller> _installers;

        private readonly Action<IWindsorContainer> _setupAction;

        private readonly Assembly[] _installerSourceAssemblies;

        public WindsorBootstrapper(IEnumerable<IWindsorInstaller> installers, Assembly[] installerSourceAssemblies=null, Action<IWindsorContainer> setupAction=null)
        {
            _installers = installers;
            _setupAction = setupAction;
            _installerSourceAssemblies = installerSourceAssemblies;
        }

        public void Configure()
        {
            var container = new WindsorContainer();
            
            var selectedInstallers = new List<IWindsorInstaller>();

            if (_installerSourceAssemblies != null)
            {
                var installers = GetInstancesOf<IWindsorInstaller>(_installerSourceAssemblies);
                selectedInstallers.AddRange(installers);
            }
            if (_installers != null)
            {
                selectedInstallers.AddRange(_installers);
            }

            if (_setupAction!=null)
            {
                _setupAction(container);
            }

            
            container.Install(selectedInstallers.ToArray());
            Result = container;
        }

        public IWindsorContainer Result { get; private set; }

        public T[] GetInstancesOf<T>(Assembly[] assemblies)
        {
            var type = typeof(T);
            var instances = new List<T>();
            foreach (var assembly in assemblies)
            {
                var assemblyInstance = (
                    assembly.GetTypes()
                    .Where(t => type.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(Activator.CreateInstance)
                    .Cast<T>()
                    ).ToArray();
                instances.AddRange(assemblyInstance);
            }
            return instances.ToArray();
        }
    }
}