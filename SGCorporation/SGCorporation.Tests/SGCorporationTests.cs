using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGCorporation.BLL;
using SGCorporation.UI;

namespace SGCorporation.Tests
{
    [TestFixture]
    public class SGCorporationTests
    {
        private Program _program;
        private OrderOperations _orderOperations;

        [SetUp]
        public void BeforeEachTest()
        {
            _program = new Program();
            Console.WriteLine("SetUp called...");
        }

        [Test]
        public void UppercaseFirst_String_ReturnString()
        {
            string result = _orderOperations.UppercaseFirst("wood");

            Assert.AreEqual("Wood", result);
        }
    }
}
