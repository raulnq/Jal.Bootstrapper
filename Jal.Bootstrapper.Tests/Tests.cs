using Jal.Bootstrapper.Impl;
using Jal.Bootstrapper.Tests.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Jal.Bootstrapper.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Configure_WithCompositeBootstrapper_ShouldBeTrue()
        {
            var bootstrapper = new DoSomethingBootstrapper();

            new CompositeBootstrapper().Add(bootstrapper).Run();

            bootstrapper.Result.ShouldBe(true);
        }
    }
}
