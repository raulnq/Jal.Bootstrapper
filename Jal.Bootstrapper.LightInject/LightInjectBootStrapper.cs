using System;
using System.Reflection;
using Jal.Bootstrapper.Interface;
using LightInject;

namespace Jal.Bootstrapper.LightInject
{
    public class LightInjectBootStrapper : IBootstrapper<ServiceContainer>
    {
        private readonly Action<ServiceContainer> _setupAction;

        private readonly Assembly[] _compositionRootSourceAssemblies;

        public LightInjectBootStrapper(Assembly[] compositionRootSourceAssemblies=null, Action<ServiceContainer> setupAction=null)
        {
            _setupAction = setupAction;

            _compositionRootSourceAssemblies = compositionRootSourceAssemblies;
        }

        public void Configure()
        {
            var container = new ServiceContainer();
            
            if (_compositionRootSourceAssemblies != null)
            {
                foreach (var compositionRootSourceAssembly in _compositionRootSourceAssemblies)
                {;
                    container.RegisterAssembly(compositionRootSourceAssembly);
                }
            }

            _setupAction?.Invoke(container);

            Result = container;
        }

        public ServiceContainer Result { get; private set; }
    }
}