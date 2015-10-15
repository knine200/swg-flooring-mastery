using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;

namespace SGCorporation.Data
{
    public class ProductRepository
    {
        private const string _filePath = @"DataFiles\Products.txt";

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string[] reader = File.ReadAllLines(_filePath);

            for (int i = 1; i < reader.Length; i++)
            {
                string[] columns = reader[i].Split(',');

                Product product = new Product();

                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(product);
            }

            return products;
        }

        public Product GetProduct(string ProductType)
        {
            List<Product> products = GetAllProducts();
            return products.FirstOrDefault(t => t.ProductType == ProductType);
        }

    }
}
