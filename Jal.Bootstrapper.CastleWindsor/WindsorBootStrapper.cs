using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.CastleWindsor
{
    public class WindsorBootstrapper : IBootstrapper<IWindsorContainer>
    {
        private readonly IEnumerable<IWindsorInstaller> _installers;

        private readonly Action<IWindsorContainer> _action;

        public WindsorBootstrapper(IEnumerable<IWindsorInstaller> installers, Action<IWindsorContainer> action=null)
        {
            _installers = installers;
            _action = action;
        }

        public WindsorBootstrapper(IWindsorContainer container, IEnumerable<IWindsorInstaller> installers, Action<IWindsorContainer> action = null)
        {
            _installers = installers;
            _action = action;
            Result = container;
        }

        public void Run()
        {
            if (Result == null)
            {
                Result = new WindsorContainer();
            }
            
            var selectedInstallers = new List<IWindsorInstaller>();

            if (_installers != null)
            {
                selectedInstallers.AddRange(_installers);
            }

            _action?.Invoke(Result);

            Result.Install(selectedInstallers.ToArray());
        }

        public IWindsorContainer Result { get; private set; }
    }
}