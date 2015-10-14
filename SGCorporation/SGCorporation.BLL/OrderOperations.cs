using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Data;
using SGCorporation.Models;

namespace SGCorporation.BLL
{
    public class OrderOperations
    {
        public Response GetAllOrders(DateTime OrderDate)
        {
            OrderRepository repo = new OrderRepository();
            Response response = new Response();

            List<Order> orders = repo.GetAllOrders(OrderDate);

            if (orders == null)
            {
                response.Success = false;
                response.Message = "This order does not exist";
            }
            else
            {
                response.Success = true;
                response.Message = "You got all the orders for the date";
            }

            foreach (var order in orders)
            {
                Console.WriteLine("OrderNumber: {0}, " +
                                  "CustomerName: {1}, " +
                                  "StateName: {2}" +
                                  "TaxRate: {3}" +
                                  "ProductType: {4}" +
                                  "Area: {5}, " +
                                  "CostPerSquareFoot: {6}, " +
                                  "LaborCostPerSquareFoot: {7}, " +
                                  "MaterialCost: {8}, " +
                                  "LaborCost: {9}, " +
                                  "Tax: {10}, " +
                                  "Total: {11}",
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
            return response;
        }

        public Response CreateOrder()
        {
            OrderRepository repo = new OrderRepository();
            Response response = new Response();
            Order newOrder = new Order();

            newOrder.OrderNumber = 1;
            Console.Write("Input the Customer Name: ");
            newOrder.CustomerName = Console.ReadLine();
            Console.Write("Input the State Abbreviation: ");
            newOrder.StateName = Console.ReadLine();
            Console.Write("Input the Tax Rate: ");
            string TaxRate = Console.ReadLine();
            newOrder.TaxRate = decimal.Parse(TaxRate);
            Console.Write("Input the Product Type: ");
            newOrder.ProductType = Console.ReadLine();
            Console.Write("Input the Area: ");
            string Area = Console.ReadLine();
            newOrder.Area = decimal.Parse(Area);
            Console.Write("Input the CostPerSquareFoot: ");
            string CostPerSquareFoot = Console.ReadLine();
            newOrder.CostPerSquareFoot = decimal.Parse(CostPerSquareFoot);
            Console.Write("Input the LaborCostPerSquareFoot: ");
            string LaborCostPerSquareFoot = Console.ReadLine();
            newOrder.LaborCostPerSquareFoot = decimal.Parse(LaborCostPerSquareFoot);

            newOrder.MaterialCost = newOrder.Area*newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area*newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost)*newOrder.TaxRate;
            newOrder.Total = newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax;

            int returnedOrderNumber = repo.WriteNewLine(newOrder);

            string input = "01012016";
            string format = "MMddyyyy";
            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            if (returnedOrderNumber == repo.GetAllOrders(date).Count)
            {
                response.Success = true;
                response.CreateOrderInfo = new CreateOrderSlip();
                response.CreateOrderInfo.OrderNumber = returnedOrderNumber;
                response.CreateOrderInfo.CustomerName = newOrder.CustomerName;
                response.CreateOrderInfo.StateName = newOrder.StateName;
                response.CreateOrderInfo.TaxRate = newOrder.TaxRate;
                response.CreateOrderInfo.ProductType = newOrder.ProductType;
                response.CreateOrderInfo.Area = newOrder.Area;
                response.CreateOrderInfo.CostPerSquareFoot = newOrder.CostPerSquareFoot;
                response.CreateOrderInfo.LaborCostPerSquareFoot = newOrder.LaborCostPerSquareFoot;
                response.CreateOrderInfo.MaterialCost = newOrder.MaterialCost;
                response.CreateOrderInfo.LaborCost = newOrder.LaborCost;
                response.CreateOrderInfo.Tax = newOrder.Tax;
                response.CreateOrderInfo.Total = newOrder.Total;
            }
            else
            {
                response.Success = false;
                response.Message = "Order creation failed. Please try again.";
            }

            return response;
        }


        public Response EditOrder(DateTime OrderDate, Order Order)
        {
            var response = new Response();

            if (Order.OrderNumber > 0)
            {
                var repo = new OrderRepository();
                repo.EditOrder(OrderDate, Order);

                response.Success = true;
                response.EditOrderInfo = new EditSlip();
                response.EditOrderInfo.CustomerName = Order.CustomerName;
                response.EditOrderInfo.StateName = Order.StateName;
                response.EditOrderInfo.TaxRate = Order.TaxRate;
                response.EditOrderInfo.Area = Order.Area;
                response.EditOrderInfo.CostPerSquareFoot = Order.CostPerSquareFoot;
                response.EditOrderInfo.LaborCostPerSquareFoot = Order.LaborCostPerSquareFoot;
                response.EditOrderInfo.MaterialCost = Order.MaterialCost;
                response.EditOrderInfo.LaborCost = Order.LaborCost;
                response.EditOrderInfo.Tax = Order.Tax;
                response.EditOrderInfo.Total = Order.Total;
                response.Message = "You have completed the editing of your Order!";

            }
            else
            {
                response.Success = false;
                response.Message = "Your Order does not exist!";
            }

            return response;
        }

        public Response RemoveOrder(DateTime OrderDate, Order Order)
        {
            var response = new Response();

            if (Order.OrderNumber > 0)
            {
                var repo = new OrderRepository();
                repo.RemoveOrder(OrderDate, Order);
                response.Success = true;
                response.Message = "You have deleted your Order!";
            }
            else
            {
                response.Success = false;
                response.Message = "Your Order number does not exist!";

            }

            return response;
        }

        public Order GetOrderNo(DateTime OrderDate, int OrderNo)
        {
            var repo = new OrderRepository();
            var order = repo.GetOrder(OrderDate, OrderNo);

            return order;
        }
    }
}
