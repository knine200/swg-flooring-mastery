using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Data;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;


namespace SGCorporation.BLL
{
    public class OrderOperations
    {
        public TaxRepository TaxRepo;
        public ProductRepository ProductRepo;
        public OrderRepositoryFactory factory;
        public ErrorLog ErrorLog;
        public Order newOrder;
        public Response response;

        public OrderOperations()
        {
            TaxRepo = new TaxRepository();
            ProductRepo = new ProductRepository();
            factory = new OrderRepositoryFactory();
            newOrder = new Order();
            response = new Response();

        }

        public Response GetAllOrders(DateTime OrderDate)
        {
            var repo = factory.CreateOrderRepository();

            Response response = new Response();

            List<Order> orders = repo.GetAllOrders(OrderDate);

            if (orders == null)
            {
                response.Success = false;
                response.Message = "There are no orders for that date";
                return response;
            }

            response.Success = true;
            response.Message = "All orders for that date have been retrieved";

            foreach (Order order in orders)
            {
                Console.WriteLine();


                Console.WriteLine("Order Number:                    {0} " +
                                  "\nCustomer Name:                   {1} " +
                                  "\nState Name:                      {2}" +
                                  "\nTax Rate:                        {3}" +
                                  "\nProduct Type:                    {4}" +
                                  "\nArea:                            {5} " +
                                  "\nCost Per Square Foot:           {6:c} " +
                                  "\nLabor Cost Per Square Foot:     {7:c} " +
                                  "\nMaterial Cost:                  {8:c} " +
                                  "\nLabor Cost:                     {9:c} " +
                                  "\nTax:                            {10:c} " +
                                  "\nTotal:                          {11:c}",
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

          //  Response response = new Response();
            //Order newOrder = new Order();
            string customerNameInput;
            string stateInput;
            string productType;
            string area;

            newOrder.OrderNumber = 1;

            do
            {
                Console.Write("Input the Customer Name: ");
                customerNameInput = Console.ReadLine();

                if (string.IsNullOrEmpty(customerNameInput))
                {
                    customerNameInput = "1";
                }

                response = ValidInputCheckString(customerNameInput);
                Console.WriteLine(response.Message);

                if (response.Success)
                {
                    newOrder.CustomerName = customerNameInput;
                }


            } while (response.Success == false || customerNameInput == "1");


            do
            {
                Console.WriteLine("(OH) Ohio     (MI) Michigan     (PA) Pennsylvania     (IN) Indiana");
                Console.Write("Input the State Abbreviation: ");
                stateInput = Console.ReadLine();
                string stateName = stateInput.ToUpper();

                if (stateName == "OH" || stateName == "MI" || stateName == "PA" ||
                    stateName == "IN")
                {
                    response = ValidInputCheckString(stateName);
                    Console.WriteLine(response.Message);


                    if (response.Success)
                    {
                        newOrder.StateName = stateName.ToUpper();
                        Tax stateTaxObject = TaxRepo.GetTax(stateName.ToUpper());
                        newOrder.TaxRate = stateTaxObject.TaxRate / 100;
                    }

                }
                else
                {
                    response = ValidInputCheckString("1");
                    Console.WriteLine();
                    Console.WriteLine("Invalid state abbreviation");
                    Console.WriteLine();
                }

            } while (response.Success == false || stateInput == null);

            do
            {
                Console.WriteLine("Carpet     Laminate     Tile     Wood");
                Console.Write("Input the Product Type: ");
                productType = Console.ReadLine();

                if (string.IsNullOrEmpty(productType))
                {
                    productType = "1";
                }


                response = ValidInputCheckString(productType);
                Console.WriteLine(response.Message);

                if (response.Success && (UppercaseFirst(productType) == "Wood" || UppercaseFirst(productType) == "Tile" || UppercaseFirst(productType) == "Carpet" ||
                    UppercaseFirst(productType) == "Laminate"))
                {

                    Product ProductTypeObject = ProductRepo.GetProduct(UppercaseFirst(productType));
                    newOrder.ProductType = ProductTypeObject.ProductType;

                    newOrder.CostPerSquareFoot = ProductTypeObject.CostPerSquareFoot;
                    newOrder.LaborCostPerSquareFoot = ProductTypeObject.LaborCostPerSquareFoot;

                }
                else
                {
                    response = ValidInputCheckString("1");
                    Console.WriteLine();
                    Console.WriteLine("Invalid product type");
                    Console.WriteLine();
                }

            } while (response.Success == false || productType == "1");

            do
            {
                Console.Write("Input the Area: ");
                area = Console.ReadLine();
                if (string.IsNullOrEmpty(area))
                {
                    area = "a";
                }

                response = ValidInputCheckDecimal(area);
                Console.WriteLine(response.Message);

                if (response.Success)
                {
                    newOrder.Area = decimal.Parse(area);

                }

            } while (response.Success == false || area == null);

            newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost) * newOrder.TaxRate;
            newOrder.Total = newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax;

            Console.WriteLine();
            Console.WriteLine("Order Number:                    {0} " +
                              "\nCustomer Name:                   {1} " +
                              "\nState Name:                      {2}" +
                              "\nTax Rate:                        {3}" +
                              "\nProduct Type:                    {4}" +
                              "\nArea:                            {5} " +
                              "\nCost Per Square Foot:           {6:c} " +
                              "\nLabor Cost Per Square Foot:     {7:c} " +
                              "\nMaterial Cost:                  {8:c} " +
                              "\nLabor Cost:                     {9:c} " +
                              "\nTax:                            {10:c} " +
                              "\nTotal:                          {11:c}",
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
                response.Message = "The order has been edited";

            }
            else
            {
                response.Success = false;
                response.Message = "That order does not exist";
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
                response.Message = "The order has been deleted";
            }
            else
            {
                response.Success = false;
                response.Message = "That order number does not exist";

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
            Response response = new Response();
            if (userInput == null)
            {
                response.Success = false;
                return response;
            }

            char[] undesirableChars =
            {
                ' ', ',', '\n', '\t', ';', '/', '\\', '|', '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9'
            };



            foreach (var item in undesirableChars)
            {
                if (userInput.Contains(item))
                {
                    response.Success = false;

                    response.Message = "Invalid entry\nOnly alphabetical characters allowed";
                    return response;
                }
            }
            response.Success = true;
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
                    response.Message = "Invalid entry\nOnly number characters allowed";
                    return response;
                }
            }
            response.Success = true;
            return response;
        }


    }
}
