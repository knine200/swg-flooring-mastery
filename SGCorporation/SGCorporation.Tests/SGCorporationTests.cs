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
using SGCorporation.Models.Interfaces;
using SGCorporation.UI;
using SGCorporation.UI.Workflows;

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
        public void GetByNo_OrderNo_ReturnOrder()
        {
            OrderRepoTest repoTest = new OrderRepoTest();

            var result = repoTest.GetByNo(1);

            Assert.AreEqual(1, result.OrderNumber);
        }

        [Test]
        public void PromptForOrderNo_ReturnInt()
        {
            RemoveOrderWorkflow rwf = new RemoveOrderWorkflow();

            int result = rwf.PromptForOrderNo();

            Assert.IsNotNull(result);
        }

        [Test]
        public void PromptForDate_ReturnDateTime()
        {
            RemoveOrderWorkflow rwf = new RemoveOrderWorkflow();

            DateTime result = rwf.PromptForDate();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProduct_ProductType_ReturnProduct()
        {
            ProductRepository repo = new ProductRepository();

            Product result = repo.GetProduct("Wood");

            Assert.AreEqual("Wood", result.ProductType);
        }

        [Test]
        public void GetTax_StateAbbreviation_ReturnTax()
        {
            TaxRepository repo = new TaxRepository();

            Tax result = repo.GetTax("IN");

            Assert.AreEqual("IN", result.StateAbbreviation);
        }
    }
}
