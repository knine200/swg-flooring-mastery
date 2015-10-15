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

            foreach (Order order in orders)
            {
                Console.WriteLine("OrderNumber: {0} " +
                                  "\nCustomerName: {1} " +
                                  "\nStateName: {2}" +
                                  "\nTaxRate: {3}" +
                                  "\nProductType: {4}" +
                                  "\nArea: {5} " +
                                  "\nCostPerSquareFoot: {6} " +
                                  "\nLaborCostPerSquareFoot: {7} " +
                                  "\nMaterialCost: {8} " +
                                  "\nLaborCost: {9} " +
                                  "\nTax: {10} " +
                                  "\nTotal: {11}",
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
            TaxRepository taxRepo = new TaxRepository();
            ProductRepository productRepo = new ProductRepository();

            newOrder.OrderNumber = 1;
            Console.Write("Input the Customer Name: ");
            newOrder.CustomerName = Console.ReadLine();
            Console.Write("Input the State Abbreviation: ");
            string stateName = Console.ReadLine();
            newOrder.StateName = stateName.ToUpper();
            Tax stateTaxObject = taxRepo.GetTax(stateName.ToUpper());
            newOrder.TaxRate = stateTaxObject.TaxRate / 100;
            
            
            Console.Write("Input the Product Type: ");
            string productType = Console.ReadLine();
            
            Product ProductTypeObject = productRepo.GetProduct(UppercaseFirst(productType));
            newOrder.ProductType = UppercaseFirst(productType);

            Console.Write("Input the Area: ");
            string Area = Console.ReadLine();
            newOrder.Area = decimal.Parse(Area);

            newOrder.CostPerSquareFoot = ProductTypeObject.CostPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = ProductTypeObject.LaborCostPerSquareFoot;

            //Console.Write("Input the CostPerSquareFoot: ");
            //string CostPerSquareFoot = Console.ReadLine();
            //newOrder.CostPerSquareFoot = decimal.Parse(CostPerSquareFoot);
            //Console.Write("Input the LaborCostPerSquareFoot: ");
            //string LaborCostPerSquareFoot = Console.ReadLine();
            //newOrder.LaborCostPerSquareFoot = decimal.Parse(LaborCostPerSquareFoot);

            newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost) * newOrder.TaxRate;
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
            Response response = new Response();

            if (Order.OrderNumber > 0)
            {
                OrderRepository repo = new OrderRepository();
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
            Response response = new Response();

            if (Order.OrderNumber > 0)
            {
                OrderRepository repo = new OrderRepository();
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
            OrderRepository repo = new OrderRepository();
            Order order = repo.GetOrder(OrderDate, OrderNo);

            return order;
        }

        public string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }


    }
}
