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
                Console.WriteLine("OrderNumber: {0}, CustomerName: {1}, Area: {2}, CostPerSquareFoot: {3}, LaborCostPerSquareFoot: {4}, MaterialCost: {5}, LaborCost: {6}, Tax: {7}, Total: {8}", order.OrderNumber,  order.CustomerName, order.Area,order.CostPerSquareFoot, order.LaborCostPerSquareFoot,  order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                
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
            Console.Write("Input the MaterialCost: ");
            string MaterialCost = Console.ReadLine();
            newOrder.MaterialCost = decimal.Parse(MaterialCost);
            Console.Write("Input the LaborCost: ");
            string LaborCost = Console.ReadLine();
            newOrder.LaborCost = decimal.Parse(LaborCost);
            Console.Write("Input the Tax: ");
            string Tax = Console.ReadLine();
            newOrder.Tax = decimal.Parse(Tax);
            Console.Write("Input the Total: ");
            string Total = Console.ReadLine();
            newOrder.Total = decimal.Parse(Total);

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


        public Response EditOrder( DateTime OrderDate, Order Order)
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
    }
}
