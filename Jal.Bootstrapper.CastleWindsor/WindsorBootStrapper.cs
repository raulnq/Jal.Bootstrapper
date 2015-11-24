using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.CastleWindsor
{
    public class WindsorBootstrapper : IBootstrapper<IWindsorContainer>
    {
        private readonly IEnumerable<IWindsorInstaller> _specificInstallers;

        private readonly Action<IWindsorContainer> _setupAction;

        private readonly string _installerTypeName;

        public WindsorBootstrapper(IEnumerable<IWindsorInstaller> specificInstallers, string installerTypeTypeName, Action<IWindsorContainer> setupAction)
        {
            _specificInstallers = specificInstallers;
            _installerTypeName = installerTypeTypeName;
            _setupAction = setupAction;
        }

        public void Configure()
        {
            var container = new WindsorContainer();
            var assemblies = AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Installer");
            var installers = AssemblyFinder.Impl.AssemblyFinder.Current.GetInstancesOf<IWindsorInstaller>(assemblies);
            var selectedInstallers = new List<IWindsorInstaller>();
            foreach (var windsorInstaller in installers)
            {
                var customAttributes = windsorInstaller.GetType().GetCustomAttributes(typeof(InstallerTagAttribute), false);
                if (customAttributes.Length>0)
                {
                    foreach (var customAttribute in customAttributes)
                    {
                        var attribute = customAttribute as InstallerTagAttribute;
                        if (attribute != null && attribute.Tag == _installerTypeName)
                        {
                            selectedInstallers.Add(windsorInstaller);
                        }
                    }
                }
            }
            if (_specificInstallers != null)
            {
                selectedInstallers.AddRange(_specificInstallers);
            }
            _setupAction(container);
            container.Install(selectedInstallers.ToArray());
            Result = container;
        }

        public IWindsorContainer Result { get; private set; }
    }
}