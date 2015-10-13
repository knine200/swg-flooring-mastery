using System;
using System.Collections.Generic;
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

        public Response CreateAccount()
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

            int returnedAccountNumber = repo.WriteNewLine(newAccount);

            if (returnedAccountNumber == repo.GetAllAccounts().Count)
            {
                response.Success = true;
                response.CreateAccountInfo = new CreateAccountSlip();
                response.CreateAccountInfo.AccountNumber = returnedAccountNumber;
                response.CreateAccountInfo.FirstName = newAccount.FirstName;
                response.CreateAccountInfo.LastName = newAccount.LastName;
                response.CreateAccountInfo.NewBalance = newAccount.Balance;
            }
            else
            {
                response.Success = false;
                response.Message = "Account creation failed. Please try again.";
            }

            return response;

        }
    }
}
