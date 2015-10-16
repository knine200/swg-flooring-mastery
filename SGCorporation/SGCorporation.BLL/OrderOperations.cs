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
        public OrderRepositoryFactory factory;
        

        //public OrderRepositoryFactory factory;

        public OrderOperations()
        {
            TaxRepo = new TaxRepository();
            ProductRepo = new ProductRepository();
            factory = new OrderRepositoryFactory();

        }

        

        public Response GetAllOrders(DateTime OrderDate)
        {
            var repo = factory.CreateOrderRepository();

            Response response = new Response();

            List<Order> orders = repo.GetAllOrders(OrderDate);

            if (orders == null)
            {
                response.Success = false;
                response.Message = "There is no orders for this date";
                return response;
            }

            response.Success = true;
            response.Message = "You got all the orders for the date";

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
            var repo = factory.CreateOrderRepository();

            Response response = new Response();
            Order newOrder = new Order();



            newOrder.OrderNumber = 1;

            do
            {
                Console.Write("Input the Customer Name: ");
                string customerNameInput = Console.ReadLine();
                response = ValidInputCheckString(customerNameInput);
                Console.WriteLine(response.Message);

                if (response.Success)
                {
                    newOrder.CustomerName = customerNameInput;
                }

            } while (response.Success == false);


            do
            {
                Console.Write("Input the State Abbreviation: ");
                string stateName = Console.ReadLine();

                if (stateName.Contains("oh") || stateName.Contains("mi") || stateName.Contains("pa") ||
                    stateName.Contains("in"))
                {
                    response = ValidInputCheckString(stateName);
                    Console.WriteLine(response.Message);


                    if (response.Success)
                    {
                        newOrder.StateName = stateName.ToUpper();
                        Tax stateTaxObject = TaxRepo.GetTax(stateName.ToUpper());
                        newOrder.TaxRate = stateTaxObject.TaxRate/100;
                    }

                }
                else
                {
                    response = ValidInputCheckString("1");
                    Console.WriteLine("Invalid state selected!!");
                }


            } while (response.Success == false);


            //Tax stateTaxObject = TaxRepo.GetTax(stateName.ToUpper());
            //newOrder.TaxRate = stateTaxObject.TaxRate / 100;

            do
            {
                Console.Write("Input the Product Type: ");
                string productType = Console.ReadLine();
                response = ValidInputCheckString(productType);
                Console.WriteLine(response.Message);

                if (response.Success)
                {

                    Product ProductTypeObject = ProductRepo.GetProduct(UppercaseFirst(productType));
                    newOrder.ProductType = ProductTypeObject.ProductType;

                    newOrder.CostPerSquareFoot = ProductTypeObject.CostPerSquareFoot;
                    newOrder.LaborCostPerSquareFoot = ProductTypeObject.LaborCostPerSquareFoot;

                }


            } while (response.Success == false);


            do
            {
                Console.Write("Input the Area: ");
                string Area = Console.ReadLine();
                response = ValidInputCheckDecimal(Area);
                Console.WriteLine(response.Message);

                if (response.Success)
                {
                    newOrder.Area = decimal.Parse(Area);

                }

            } while (response.Success == false);

            

            //newOrder.CostPerSquareFoot = ProductTypeObject.CostPerSquareFoot;
            //newOrder.LaborCostPerSquareFoot = ProductTypeObject.LaborCostPerSquareFoot;



            newOrder.MaterialCost = newOrder.Area*newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area*newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost)*newOrder.TaxRate;
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


            if (repo == new OrderRepository())
            {
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
            }
            return response;
            
        }


        public Response EditOrder(DateTime OrderDate, Order Order)
        {
            Response response = new Response();

            if (Order.OrderNumber > 0)
            {
                var repo = factory.CreateOrderRepository();

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
                var repo = factory.CreateOrderRepository();

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
            var repo = factory.CreateOrderRepository();
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

        public Response ValidInputCheckString(string userInput)
        {
            char[] undesirableChars =
            {
                ' ', ',', '\n', '\t', ';', '/', '\\', '|', '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9'
            };

            Response response = new Response();

            foreach (var item in undesirableChars)
            {
                if (userInput.Contains(item))
                {
                    response.Success = false;
                    
                    response.Message = "Invalid entry. Only alphabetical characters allowed. Try again.";
                    return response;
                }
            }
            response.Success = true;
            //response.Message = "Valid entry";
            return response;
        }

        public Response ValidInputCheckDecimal(string userInput)
        {
            char[] undesirableChars =
            {
                ' ', ',', '\n', '\t', ';', '/', '\\', '|', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
                'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'x', 'y', 'z',
            };

            Response response = new Response();

            foreach (var item in undesirableChars)
            {
                if (userInput.Contains(item))
                {
                    response.Success = false;
                    response.Message = "Invalid entry. Only number characters allowed. Try again.";
                    return response;
                }
            }
            response.Success = true;
            //response.Message = "Valid entry";
            return response;
        }


    }
}
