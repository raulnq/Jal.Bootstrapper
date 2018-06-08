using System;
using System.Linq;
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

        private readonly ICompositionRoot[] _compositionroots;

        public LightInjectBootStrapper(Assembly[] compositionRootSourceAssemblies=null, Action<ServiceContainer> setupAction=null, ContainerOptions options=null)
        {
            _setupAction = setupAction;

            _compositionRootSourceAssemblies = compositionRootSourceAssemblies;

            _options = options;
        }

        public LightInjectBootStrapper(ICompositionRoot[] compositionroots = null, Action<ServiceContainer> setupAction = null, ContainerOptions options = null)
        {
            _setupAction = setupAction;

            _compositionroots = compositionroots;

            _options = options;
        }

        public void Register(ICompositionRoot composition, ServiceContainer container)
        {
            var method = typeof(ServiceContainer).GetMethods().First(x => x.Name == nameof(ServiceContainer.RegisterFrom) && !x.GetParameters().Any());

            var genericmethod = method?.MakeGenericMethod(composition.GetType());

            genericmethod?.Invoke(container, new object[] { });
        }

        public void Configure()
        {
            var container = (_options == null) ? new ServiceContainer() : new ServiceContainer(_options);

            if (_compositionroots != null)
            {
                foreach (var compositionroot in _compositionroots)
                {
                    Register(compositionroot, container);
                }
            }

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