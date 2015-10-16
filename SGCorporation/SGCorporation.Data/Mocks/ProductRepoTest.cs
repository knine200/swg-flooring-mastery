using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;

namespace SGCorporation.Data.Mocks
{
    public class ProductRepoTest : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            return new List<Product>()
           {
               new Product() {ProductType = "Hardwood", CostPerSquareFoot = 7.55M, LaborCostPerSquareFoot = 4.12M},
               new Product() {ProductType = "Laminate", CostPerSquareFoot = 5.48M, LaborCostPerSquareFoot = 2.96M}
           };
        }

        public Product GetProduct(string ProductType)
        {
            List<Product> products = GetAllProducts();
            return products.FirstOrDefault(t => t.ProductType == ProductType);
        }
    }
}
