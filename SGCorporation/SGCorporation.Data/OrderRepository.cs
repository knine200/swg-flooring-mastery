using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
        //private string _value = ConfigurationManager.AppSettings["Option"];

        public string GetFilePath(DateTime OrderDate)
        {
            //if (_value == "Test")
            //{



            //    //File names vary depending on the date.
            //    //File names are always in the following format Orders_MMDDYYYY.
            //    //The DateTime object is accepted and converted to a string.
            //    //Then it's concatenated to form the file path.
            string ordersFilePath = @"DataFiles\Orders_" + OrderDate.ToString("MMddyyyy") + ".txt";
            return ordersFilePath;
            //}
            //else
            //{
            //string ordersFilePath = @"DataFiles\Orders_" + OrderDate.ToString("MMddyyyy") + ".txt";
            //return ordersFilePath;
            //}
        }

        public List<Order> GetAllOrders(DateTime OrderDate)
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

        public Order GetOrder(DateTime OrderDate, int OrderNo)
        {
            List<Order> orders = GetAllOrders(OrderDate);

            return orders.FirstOrDefault(x => x.OrderNumber == OrderNo);
        }

        public void RemoveOrder(DateTime OrderDate, Order OrderToUpdate)
        {
            List<Order> orders = GetAllOrders(OrderDate);

            Order order = orders.First(a => a.OrderNumber == OrderToUpdate.OrderNumber);
            orders.Remove(order);

            OverwriteFile(orders, OrderDate);
        }

        public void EditOrder(DateTime OrderDate, Order OrderToUpdate)
        {
            List<Order> orders = GetAllOrders(OrderDate);

            Order order = orders.First(a => a.OrderNumber == OrderToUpdate.OrderNumber);
            order.CustomerName = OrderToUpdate.CustomerName;
            order.StateName = OrderToUpdate.StateName;
            order.TaxRate = OrderToUpdate.TaxRate;
            order.ProductType = OrderToUpdate.ProductType;
            order.Area = OrderToUpdate.Area;
            order.CostPerSquareFoot = OrderToUpdate.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = OrderToUpdate.LaborCostPerSquareFoot;
            order.MaterialCost = OrderToUpdate.MaterialCost;
            order.LaborCost = OrderToUpdate.LaborCost;
            order.Tax = OrderToUpdate.Tax;
            order.Total = OrderToUpdate.Total;

            OverwriteFile(orders, OrderDate);
        }

        public void OverwriteFile(List<Order> orders, DateTime OrderDate)
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
                        order.LaborCost,
                        order.Tax,
                        order.Total);
                }
            }
        }
        public int WriteNewLine(Order Order)
        {
            string input = "01012016";
            string format = "MMddyyyy";
            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            List<Order> orders = GetAllOrders(date);


            int newOrderNo = orders.Max(a => a.OrderNumber) + 1;

            using (StreamWriter writer = File.AppendText(GetFilePath(date)))
            {
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                    newOrderNo,
                    Order.CustomerName,
                    Order.StateName,
                    Order.TaxRate,
                    Order.ProductType,
                    Order.Area,
                    Order.CostPerSquareFoot,
                    Order.LaborCostPerSquareFoot,
                    Order.MaterialCost,
                    Order.LaborCost,
                    Order.Tax,
                    Order.Total);
            }
            return newOrderNo;
        }
    }
}
