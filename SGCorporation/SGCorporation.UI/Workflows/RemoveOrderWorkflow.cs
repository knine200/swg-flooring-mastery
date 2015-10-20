using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.BLL;
using SGCorporation.Models;

namespace SGCorporation.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderOperations ops = new OrderOperations();
            Order orderToRemove = new Order();
            Response response1 = new Response();

            DateTime currentDate;

            do
            {
                currentDate = PromptForDate();
                response1 = ops.GetAllOrders(currentDate);
                if (response1.Success == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input format or that date does not exist");
                    Console.WriteLine();

                    WrongUserInputLog log = new WrongUserInputLog()
                    {
                        ErrorTime = DateTime.Now,
                        Message = $"Remove An Order -> Invalid user input for date"
                    };
                    ops.PassOnWrongUserInput(log);

                }
            } while (response1.Success == false);

            do
            {
                int orderNo = PromptForOrderNo();
                orderToRemove = ops.GetOrderNo(currentDate, orderNo);
                if (orderToRemove == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("That order number does not exist");
                    WrongUserInputLog log = new WrongUserInputLog()
                    {
                        ErrorTime = DateTime.Now,
                        Message = $"Remove An Order -> Order number does not exist"
                    };
                    ops.PassOnWrongUserInput(log);
                    Console.WriteLine();
                }
            } while (orderToRemove == null);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Order Number:                    {0} " +
                                  "\nCustomer Name:                   {1} " +
                                  "\nState Name:                      {2} " +
                                  "\nTax Rate:                        {3} " +
                                  "\nProduct Type:                    {4} " +
                                  "\nArea:                            {5} " +
                                  "\nCost Per Square Foot:           {6:c} " +
                                  "\nLabor Cost Per Square Foot:     {7:c} " +
                                  "\nMaterial Cost:                  {8:c} " +
                                  "\nLabor Cost:                     {9:c} " +
                                  "\nTax:                            {10:c} " +
                                  "\nTotal:                          {11:c} ",
                                  orderToRemove.OrderNumber,
                                  orderToRemove.CustomerName,
                                  orderToRemove.StateName,
                                  orderToRemove.TaxRate,
                                  orderToRemove.ProductType,
                                  orderToRemove.Area,
                                  orderToRemove.CostPerSquareFoot,
                                  orderToRemove.LaborCostPerSquareFoot,
                                  orderToRemove.MaterialCost,
                                  orderToRemove.LaborCost,
                                  orderToRemove.Tax,
                                  orderToRemove.Total);

            Console.WriteLine();
            Console.Write("Are you sure you want to remove the above order? (Y/N) ");
            string response = Console.ReadLine();

            if (response.ToUpper() == "Y")
            {
                ops.RemoveOrder(currentDate, orderToRemove);
                Console.WriteLine();
                Console.WriteLine("The order has been removed");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Order removal process cancelled");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public DateTime PromptForDate()
        {
            Console.Write("Enter the date for your order (MM/DD/YYYY): ");

            string input = Console.ReadLine();

            DateTime date;

            DateTime.TryParse(input, out date);
            
            return date.Date;

        }
        
        public int PromptForOrderNo()
        {
            Console.Write("Enter the order number: ");
            string input = Console.ReadLine();

            int OrderNo;
            if (int.TryParse(input, out OrderNo))
            {
                return OrderNo;
            }

            return OrderNo;
        }
    }
}
