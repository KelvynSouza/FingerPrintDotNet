using NUnit.Framework;
using System;

namespace FingerPrint.Tests.NamesPace2
{
    [TestFixture]
    public class TestsClass2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test,Order(1)]
        public void Test3()
        {
            var a = "a";
            Assert.AreEqual("a", a);
        }
        [Test,Order(2)]
        public void Test4()
        {
            var b = "b";
            Console.WriteLine("Testando Output");
            Assert.AreNotEqual("b", b);
        }
    }
}