using Jal.Bootstrapper.Impl;
using Jal.Bootstrapper.Interface;
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

            var composite = new CompositeBootstrapper(new IBootstrapper[] { bootstrapper });

            composite.Configure();

            composite.Result.ShouldBe(true);

            bootstrapper.Result.ShouldBe(true);
        }
    }
}
