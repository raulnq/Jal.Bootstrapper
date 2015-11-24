using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.Tests.Impl
{
    public class DoSomethingBootstrapper : IBootstrapper<bool>
    {
        public void Configure()
        {
            Result = true;
        }

        public bool Result { get; private set; }
    }
}
