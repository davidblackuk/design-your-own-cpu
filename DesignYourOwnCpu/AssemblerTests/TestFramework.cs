using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace AssemblerTests
{
    [ExcludeFromCodeCoverage]
    public class TestFramework
    {
        [Test]
        public void TestUnitTestFrameworkIsWorking()
        {
            Assert.Pass();
        }
    }
}