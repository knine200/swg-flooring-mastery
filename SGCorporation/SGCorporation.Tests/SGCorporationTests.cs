using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGCorporation.BLL;
using SGCorporation.Data;
using SGCorporation.Models;
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
        [TestCase("tile", "Tile")]
        [TestCase("carpet", "Carpet")]
        public void UppercaseFirst_String_ReturnString(string a, string b)
        {
            OrderOperations ops = new OrderOperations();

            string result = ops.UppercaseFirst(a);

            Assert.AreEqual(b, result);
        }

        [Test]
        public void ReturnProduct_String_ReturnProduct()
        {
            OrderOperations ops = new OrderOperations();

            Product result = ops.ReturnProduct("Tile");

            Assert.AreEqual("Tile", result.ProductType);
        }

        [Test]
        public void ReturnTax_String_ReturnTax()
        {
            OrderOperations ops = new OrderOperations();

            Tax result = ops.ReturnTax("IN");

            Assert.AreEqual("IN", result.StateAbbreviation);
        }

        [Test]
        public void ValidInputCheckString_String_ReturnResponse()
        {
            OrderOperations ops = new OrderOperations();

            Response result = ops.ValidInputCheckString("james");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void ValidInputCheckDecimal_String_ReturnResponse()
        {
            OrderOperations ops = new OrderOperations();

            Response result = ops.ValidInputCheckDecimal("james");

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void GetOrderNo_OrderDateAndOrderNo_ReturnOrder()
        {
            OrderOperations ops = new OrderOperations();

            Order result = ops.GetOrderNo(DateTime.Parse("01012016").Date, 1);

            Assert.AreEqual(1, result.OrderNumber);
        }
    }
}
