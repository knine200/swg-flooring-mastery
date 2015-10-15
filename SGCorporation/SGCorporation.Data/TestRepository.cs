using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;

namespace SGCorporation.Data
{
    public class TestRepository: IOrderRepository

    {
        private static string test = ConfigurationManager.AppSettings["Option"];

        private const string _filePath = @"DataFiles\Orders_06012013.txt";

        public List<Order> GetAllOrders(DateTime OrderDate)
        {
            List<Order> orders = new List<Order>();

            string ordersFilePath = _filePath;

            if (!File.Exists(ordersFilePath))
            {
                return null;
            }

            string[] reader = File.ReadAllLines(ordersFilePath);

            for (int i = 1; i < reader.Length; i++)
            {
                string[] columns = reader[i].Split(',');

                Order order = new Order();

                order.OrderNumber = int.Parse(columns[0]);
                order.CustomerName = columns[1];
                order.StateName = columns[2];
                order.TaxRate = decimal.Parse(columns[3]);
                order.ProductType = columns[4];
                order.Area = decimal.Parse(columns[5]);
                order.CostPerSquareFoot = decimal.Parse(columns[6]);
                order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                order.MaterialCost = decimal.Parse(columns[8]);
                order.LaborCost = decimal.Parse(columns[9]);
                order.Tax = decimal.Parse(columns[10]);
                order.Total = decimal.Parse(columns[11]);

                orders.Add(order);
            }
            return orders;
        }

        public Order GetOrder(DateTime OrderDate, int OrderNo)
        {
            List<Order> orders = GetAllOrders(OrderDate);

            return orders.FirstOrDefault(x => x.OrderNumber == OrderNo);
        }



    }
}
