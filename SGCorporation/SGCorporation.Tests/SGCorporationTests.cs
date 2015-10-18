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

      

        [SetUp]
        public void BeforeEachTest()
        {
            _program = new Program();
            Console.WriteLine("SetUp called...");
        }


        [TestCase("wood", "Wood")]

        public void UppercaseFirst_String_ReturnString(string a, string b)
        {
            OrderOperations target = new OrderOperations();

            string result = target.UppercaseFirst(a);

            Assert.AreEqual(b, result);
        }
    }
}
