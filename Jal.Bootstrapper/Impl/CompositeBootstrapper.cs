using System.Collections.Generic;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.Impl
{
    public class CompositeBootstrapper : IBootstrapper<bool>
    {
        private readonly IEnumerable<IBootstrapper> _bootStrappers;

        public CompositeBootstrapper(IEnumerable<IBootstrapper> bootStrappers)
        {
            _bootStrappers = bootStrappers;
        }

        public void Configure()
        {
            foreach (var bootStrapper in _bootStrappers)
            {
                bootStrapper.Configure();
            }

            Result = true;
        }

        public bool Result { get; private set; }
    }
}