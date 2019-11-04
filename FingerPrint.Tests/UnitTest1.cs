using NUnit.Framework;

namespace FingerPrint.Tests.NamesPace1
{
    [TestFixture]
    public class TestsClass1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test,Order(1)]
        public void Test1()
        {
            var a = "a";
            Assert.AreEqual("a", a);
        }
        [Test,Order(2)]
        public void Test2()
        {
            var b = "b";
            Assert.AreNotEqual("b", b);
        }
    }
}