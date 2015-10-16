using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Data;
using SGCorporation.Data.Mocks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;


namespace SGCorporation.BLL
{
    public class OrderOperations
    {
        public TaxRepository TaxRepo;
        public ProductRepository ProductRepo;
        public TaxRepoTest TaxRepoTest;
        public ProductRepoTest ProductRepoTest;
       
        //public OrderRepositoryFactory factory;

        public OrderOperations()
        {
            TaxRepo = new TaxRepository();
            ProductRepo = new ProductRepository();
            //factory = new OrderRepositoryFactory();

        }

        //public void DetermineProdOrTestMode()
        //{
            
        //}

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
                Console.WriteLine();
                
                
                Console.WriteLine("OrderNumber: {0} " +
                                  "\nCustomerName: {1} " +
                                  "\nStateName: {2}" +
                                  "\nTaxRate: {3}" +
                                  "\nProductType: {4}" +
                                  "\nArea: {5} " +
                                  "\nCostPerSquareFoot: {6:c} " +
                                  "\nLaborCostPerSquareFoot: {7:c} " +
                                  "\nMaterialCost: {8:c} " +
                                  "\nLaborCost: {9:c} " +
                                  "\nTax: {10:c} " +
                                  "\nTotal: {11:c}",
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
                Console.WriteLine();

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
            string stateName = Console.ReadLine();
            newOrder.StateName = stateName.ToUpper();

            Tax stateTaxObject = TaxRepo.GetTax(stateName.ToUpper());
            newOrder.TaxRate = stateTaxObject.TaxRate / 100;
            
            
            Console.Write("Input the Product Type: ");
            string productType = Console.ReadLine();
            
            Product ProductTypeObject = ProductRepo.GetProduct(UppercaseFirst(productType));
            newOrder.ProductType = UppercaseFirst(productType);

            Console.Write("Input the Area: ");
            string Area = Console.ReadLine();
            newOrder.Area = decimal.Parse(Area);

            newOrder.CostPerSquareFoot = ProductTypeObject.CostPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = ProductTypeObject.LaborCostPerSquareFoot;

            

            newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost) * newOrder.TaxRate;
            newOrder.Total = newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax;

            Console.WriteLine();
            Console.WriteLine("OrderNumber: {0} " +
                                  "\nCustomerName: {1} " +
                                  "\nStateName: {2}" +
                                  "\nTaxRate: {3}" +
                                  "\nProductType: {4}" +
                                  "\nArea: {5} " +
                                  "\nCostPerSquareFoot: {6:c} " +
                                  "\nLaborCostPerSquareFoot: {7:c} " +
                                  "\nMaterialCost: {8:c} " +
                                  "\nLaborCost: {9:c} " +
                                  "\nTax: {10:c} " +
                                  "\nTotal: {11:c}",
                                  newOrder.OrderNumber,
                                  newOrder.CustomerName,
                                  newOrder.StateName,
                                  newOrder.TaxRate,
                                  newOrder.ProductType,
                                  newOrder.Area,
                                  newOrder.CostPerSquareFoot,
                                  newOrder.LaborCostPerSquareFoot,
                                  newOrder.MaterialCost,
                                  newOrder.LaborCost,
                                  newOrder.Tax,
                                  newOrder.Total);

            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Please confirm your order creation? (Y/N) ");
            string userInput = Console.ReadLine();

            if (userInput.ToUpper() == "Y")
            {
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
            }          
            else
            {
                response.Success = false;
                response.Message = "Order creation cancelled";
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

        public Tax ReturnTax(string stateName)
        {
            Tax stateTaxObject = TaxRepo.GetTax(stateName.ToUpper());

            return stateTaxObject;

        }

        public Product ReturnProduct(string productName)
        {
            Product stateProductObject = ProductRepo.GetProduct(UppercaseFirst(productName));

            return stateProductObject;

        }

    }
}
