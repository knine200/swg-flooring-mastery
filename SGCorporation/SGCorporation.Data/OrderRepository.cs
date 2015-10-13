using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;
using System.IO;

namespace SGCorporation.Data
{
    public class OrderRepository : IOrderRepository
    {
        public string GetFilePath(string OrderDate)
        {
            //File names vary depending on the date.
            //File names are always in the following format Orders_MMDDYYYY.
            //The DateTime object is accepted and converted to a string.
            //Then it's concatenated to form the file path.
            string ordersFilePath = @"DataFiles\Orders_" + OrderDate + ".txt";

            return ordersFilePath;
        }

        public List<Order> GetAllOrders(string OrderDate)
        {
            List<Order> orders = new List<Order>();

            string ordersFilePath = GetFilePath(OrderDate);

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

        public Order GetOrder(OrderSlip OrderDate)
        {
            List<Order> orders = GetAllOrders(OrderDate.OrderDate);

            return orders.FirstOrDefault(x => x.OrderNumber == OrderDate.Order.OrderNumber);
        }

        public void OverwriteFile(List<Order> orders, string OrderDate)
        {
            string ordersFilePath = GetFilePath(OrderDate);

            using (StreamWriter writer = File.CreateText(ordersFilePath))
            {
                writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                foreach (var order in orders)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        order.OrderNumber,
                        order.CustomerName,
                        order.StateName,
                        order.TaxRate,
                        order.ProductType,
                        order.Area,
                        order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot,
                        order.MaterialCost,
                        order.LaborCostPerSquareFoot,
                        order.Tax,
                        order.Total);
                }
            }
        }
    }
}
