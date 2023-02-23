using NUnit.Framework;

namespace DevKit.DIoC.Tests
{
    public class MaestroTest
    {
        [SetUp]
        public void Map()
        {
            Assert.That(Maestro.Default, Is.Not.Null);
        }
    }
}
