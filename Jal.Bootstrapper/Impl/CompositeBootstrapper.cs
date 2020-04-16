using System.Collections.Generic;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.Impl
{
    public class CompositeBootstrapper : IBootstrapper
    {
        private readonly List<IBootstrapper> _bootStrappers;

        public CompositeBootstrapper(IEnumerable<IBootstrapper> bootStrappers)
        {
            _bootStrappers = new List<IBootstrapper>(bootStrappers);
        }

        public CompositeBootstrapper()
        {
            _bootStrappers = new List<IBootstrapper>();
        }

        public CompositeBootstrapper Add(IBootstrapper bootstrapper)
        {
            _bootStrappers.Add(bootstrapper);

            return this;
        }


        public void Run()
        {
            foreach (var bootStrapper in _bootStrappers)
            {
                bootStrapper.Run();
            }
        }
    }
}