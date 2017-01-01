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

        private IWindsorContainer _container;

        public WindsorBootstrapper(IEnumerable<IWindsorInstaller> installers, Assembly[] installerSourceAssemblies=null, Action<IWindsorContainer> setupAction=null)
        {
            _installers = installers;
            _setupAction = setupAction;
            _installerSourceAssemblies = installerSourceAssemblies;
        }

        public WindsorBootstrapper(IWindsorContainer container, IEnumerable<IWindsorInstaller> installers, Assembly[] installerSourceAssemblies = null, Action<IWindsorContainer> setupAction = null)
        {
            _installers = installers;
            _setupAction = setupAction;
            _installerSourceAssemblies = installerSourceAssemblies;
            _container = container;
        }

        public void Configure()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
            }
            
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

            _setupAction?.Invoke(_container);


            _container.Install(selectedInstallers.ToArray());
        }

        public IWindsorContainer Result => _container;

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