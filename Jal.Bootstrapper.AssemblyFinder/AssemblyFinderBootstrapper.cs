using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.AssemblyFinder
{
    public class AssemblyFinderBootstrapper : IBootstrapper<bool>
    {
        private readonly string _directory;

        public AssemblyFinderBootstrapper(string directory)
        {
            _directory = directory;
        }

        public void Configure()
        {
            Jal.AssemblyFinder.Impl.AssemblyFinder.Current = new Jal.AssemblyFinder.Impl.AssemblyFinder(_directory);

            Result = true;
        }

        public bool Result { get; private set; }
    }
}