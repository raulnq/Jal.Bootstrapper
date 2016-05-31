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

        public WindsorBootstrapper(IEnumerable<IWindsorInstaller> installers, Action<IWindsorContainer> setupAction, Assembly[] installerSourceAssemblies)
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
                foreach (var windsorInstaller in installers)
                {
                    var customAttributes = windsorInstaller.GetType().GetCustomAttributes(typeof(InstallerTagAttribute), false);
                    if (customAttributes.Length > 0)
                    {
                        foreach (var customAttribute in customAttributes)
                        {
                            var attribute = customAttribute as InstallerTagAttribute;
                            if (attribute != null)
                            {
                                selectedInstallers.Add(windsorInstaller);
                            }
                        }
                    }
                }
            }
            if (_installers != null)
            {
                selectedInstallers.AddRange(_installers);
            }
            _setupAction(container);
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