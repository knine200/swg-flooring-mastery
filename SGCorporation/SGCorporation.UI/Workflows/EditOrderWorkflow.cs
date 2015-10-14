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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            DateTime currentDate = PromptForDate();
            int orderNo = PromptForOrderNo();

            OrderOperations ops = new OrderOperations();

            Order order = ops.GetOrderNo(currentDate, orderNo);

            order.CustomerName = PromptToEditStrings("Customer Name", order.CustomerName);
            order.StateName = PromptToEditStrings("StateName", order.StateName);
            order.TaxRate = PromptToEditDecimal("Tax Rate", order.TaxRate);
            order.ProductType = PromptToEditStrings("Product Type", order.ProductType);
            order.Area = PromptToEditDecimal("Area", order.Area);
            order.CostPerSquareFoot = PromptToEditDecimal("Cost Per Square Foot", order.CostPerSquareFoot);
            order.LaborCostPerSquareFoot = PromptToEditDecimal("Labor Cost Per Square Foot", order.LaborCostPerSquareFoot);

            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.Tax = (order.MaterialCost + order.LaborCost) * order.TaxRate;
            order.Total = order.MaterialCost + order.LaborCost + order.Tax;

            ops.EditOrder(currentDate, order);
        }
        public DateTime PromptForDate()
        {
            Console.Write("Enter the date for your Order Date (MMDDYYYY): ");
            string input = Console.ReadLine();
            string format = "MMddyyyy";

            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            return date;
        }

        public int PromptForOrderNo()
        {
            Console.Write("Enter the order number: ");
            string input = Console.ReadLine();

            int OrderNo = int.Parse(input);

            return OrderNo;
        }

        public string PromptToEditStrings(string propertyName, string propertyValue)
        {
            bool confirmation;
            do
            {
                Console.WriteLine("Would you like to edit {0}? (Y/N)", propertyName);
                string answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    confirmation = true;
                }
                else
                {
                    string reply = "You declined to edit " + propertyName;
                    return reply;
                }
            } while (confirmation != true);

            switch (propertyName)
            {
                case "CustomerName":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input = Console.ReadLine();
                    decimal inputAmount;
                    if (decimal.TryParse(input, out inputAmount))
                    {
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input == propertyValue)
                    {
                        return propertyValue;
                    }
                    return input;
                case "StateName":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input1 = Console.ReadLine();
                    if (decimal.TryParse(input1, out inputAmount))
                    {
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input1 == propertyValue)
                    {
                        return propertyValue;
                    }
                    return input1;
                case "ProductType":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input2 = Console.ReadLine();
                    if (decimal.TryParse(input2, out inputAmount))
                    {
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input2 == propertyValue)
                    {
                        return propertyValue;
                    }
                    return input2;

                default:
                    Console.WriteLine("Invalid string");
                    return Console.ReadLine();
            }
        }

        public decimal PromptToEditDecimal(string propertyName, decimal propertyValue)
        {
            bool confirmation;
            do
            {
                Console.WriteLine("Would you like to edit {0}? (Y/N)", propertyName);
                string answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    confirmation = true;
                }
                else
                {
                    Console.WriteLine("You declined to edit {0}", propertyName);

                    return 0;
                }
            } while (confirmation != true);

            switch (propertyName)
            {
                case "Area":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input = Console.ReadLine();
                    decimal inputAmount;
                    if (decimal.TryParse(input, out inputAmount))
                    {
                        if (inputAmount == propertyValue)
                        {
                            return propertyValue;
                        }

                        return inputAmount;
                    }
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
                case "CostPerSquareFoot":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input1 = Console.ReadLine();
                    decimal inputAmount1;
                    if (decimal.TryParse(input1, out inputAmount1))
                    {
                        if (inputAmount1 == propertyValue)
                        {
                            return propertyValue;
                        }

                        return inputAmount1;
                    }
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
                case "LaborCostPerSquareFoot":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input2 = Console.ReadLine();
                    decimal inputAmount2;
                    if (decimal.TryParse(input2, out inputAmount2))
                    {
                        if (inputAmount2 == propertyValue)
                        {
                            return propertyValue;
                        }

                        return inputAmount2;
                    }
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
                case "TaxRate":
                    Console.WriteLine("Enter a new {0}: ", propertyName);
                    string input3 = Console.ReadLine();
                    decimal inputAmount3;
                    if (decimal.TryParse(input3, out inputAmount3))
                    {
                        if (inputAmount3 == propertyValue)
                        {
                            return propertyValue;
                        }

                        return inputAmount3;
                    }
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
                default:
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
            }
        }
    }
}
