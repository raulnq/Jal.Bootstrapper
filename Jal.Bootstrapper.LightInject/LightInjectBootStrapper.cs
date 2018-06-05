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

        private readonly ContainerOptions _options;

        public LightInjectBootStrapper(Assembly[] compositionRootSourceAssemblies=null, Action<ServiceContainer> setupAction=null, ContainerOptions options=null)
        {
            _setupAction = setupAction;

            _compositionRootSourceAssemblies = compositionRootSourceAssemblies;

            _options = options;
        }

        public void Configure()
        {
            var container = (_options == null) ? new ServiceContainer() : new ServiceContainer(_options);
            
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